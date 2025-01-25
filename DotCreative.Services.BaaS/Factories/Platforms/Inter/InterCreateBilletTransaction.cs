using DotCreative.Services.BaaS.Domain.Entities;
using DotCreative.Services.BaaS.Domain.Enums;
using DotCreative.Services.BaaS.Domain.ExtensionMethods;
using DotCreative.Services.BaaS.Factories.Interfaces;
using DotCreative.Services.BaaS.Factories.Platforms.Abstractions;
using Newtonsoft.Json;
using System.Text;

namespace DotCreative.Services.BaaS.Factories.Platforms.Inter;

public class InterCreateBilletTransaction : PlatformAbstract, ITransactionRequest
{
  public StringContent PrepareBilletData(TransactionRequest transactionInfo)
  {
    var discountDateTime = new DateTime(transactionInfo.Discount.Date.Year, transactionInfo.Discount.Date.Month, transactionInfo.Discount.Date.Day);
    var dueDateTime = new DateTime(transactionInfo.DueDate.Year, transactionInfo.DueDate.Month, transactionInfo.DueDate.Day);

    var billetData = new
    {
      seuNumero = DateTime.Now.ToString("yyMMddhhmm"),
      valorNominal = transactionInfo.Amount,
      dataVencimento = transactionInfo.DueDate.ToString("yyyy-MM-dd"),
      numDiasAgenda = transactionInfo.DaysOfCancel,
      pagador = new
      {
        email = transactionInfo.Payer.Email,
        ddd = transactionInfo.Payer.Contact.DDD.OnlyNumbers(),
        telefone = transactionInfo.Payer.Contact.Telephone.OnlyNumbers(),
        numero = transactionInfo.Payer.Address.Number,
        complemento = transactionInfo.Payer.Address.Complement,
        cpfCnpj = transactionInfo.Payer.Document,
        tipoPessoa = transactionInfo.Payer.Personality == EPersonality.PF ? "FISICA" : "JURIDICA",
        nome = transactionInfo.Payer.Name,
        endereco = transactionInfo.Payer.Address.Street,
        bairro = transactionInfo.Payer.Address.Neighborhood,
        cidade = transactionInfo.Payer.Address.City,
        uf = transactionInfo.Payer.Address.State,
        cep = transactionInfo.Payer.Address.Zipcode.OnlyNumbers()
      },
      desconto = transactionInfo.Discount is null ? null : new
      {
        codigo = transactionInfo.Discount.ApplyPercentage ? "PERCENTUALDATAINFORMADA" : "VALORFIXODATAINFORMADA",
        taxa = transactionInfo.Discount.ApplyPercentage ? transactionInfo.Discount.Amount : 0,
        valor = !transactionInfo.Discount.ApplyPercentage ? transactionInfo.Discount.Amount : 0,
        quantidadeDias = dueDateTime.Subtract(discountDateTime).TotalDays,
      },
      multa = transactionInfo.Penalty is null ? null : new
      {
        codigo = transactionInfo.Penalty.ApplyPercentage ? "PERCENTUAL" : "VALORFIXO",
        taxa = transactionInfo.Penalty.ApplyPercentage ? transactionInfo.Penalty.Amount : 0,
        valor = !transactionInfo.Penalty.ApplyPercentage ? transactionInfo.Penalty.Amount : 0
      },
      mensagem = new
      {
        linha1 = transactionInfo.Messages.One.Truncate(78),
        linha2 = transactionInfo.Messages.Two.Truncate(78),
        linha3 = transactionInfo.Messages.Tree.Truncate(78),
        linha4 = transactionInfo.Messages.Four.Truncate(78),
        linha5 = transactionInfo.Messages.Five.Truncate(78)
      }
    };

    StringContent jsonContent = new(
                      System.Text.Json.JsonSerializer.Serialize(billetData),
                      Encoding.UTF8,
                      "application/json"
                      );

    return jsonContent;
  }

  public async Task<string> RequestBilletFileAsync(string transactionCode, PlatformResponse platformResult)
  {
    var clientHandler = new HttpClientHandler();
    clientHandler.ClientCertificates.Add(platformResult.Certificate);
    clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;

    using (HttpClient client = new HttpClient(clientHandler))
    {
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Add("Authorization", "Bearer " + platformResult.BearerToken);

      string uriSufix = GetEndpoint("Inter_Boleto_Arquivo");     
      string URI_Pdf_Cobranca = string.Format($"{_baseUrl}/{uriSufix}", transactionCode);

      HttpResponseMessage response_pdf = await client.GetAsync(URI_Pdf_Cobranca);
      if (response_pdf.IsSuccessStatusCode)
      {
        string resultRequestChargeFile = await response_pdf.Content.ReadAsStringAsync();
        Console.WriteLine("Received response: {0}", resultRequestChargeFile);

        dynamic? chargeData = JsonConvert.DeserializeObject(resultRequestChargeFile) ?? null;
        return chargeData.pdf;
      }
      else
      {
        Console.WriteLine("Error, received status code {0}: {1}", response_pdf.StatusCode, response_pdf.ReasonPhrase);
        throw new Exception($"Error, received status code {response_pdf.Content}");
      }
    }
  }

  public async Task<TransactionResponse?> ExecuteAsync(TransactionRequest transactionInfo)
  {
    InterRequestBilletDataTransaction requestTransactionData = new InterRequestBilletDataTransaction();

    string uriSufix = GetEndpoint("Inter_Boleto_Criar");
    string URI_Emitir_Cobranca = $"{_baseUrl}/{uriSufix}";

    var clientHandler = new HttpClientHandler();
    clientHandler.ClientCertificates.Add(transactionInfo.Platform.Certificate);
    clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;

    using (HttpClient client = new HttpClient(clientHandler))
    {
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Add("Authorization", "Bearer " + transactionInfo.Platform.BearerToken);

      var response = await client.PostAsync(URI_Emitir_Cobranca, PrepareBilletData(transactionInfo));
      if (response.IsSuccessStatusCode)
      {
        string resultCreateCharge = response.Content.ReadAsStringAsync().Result;
        Console.WriteLine("Received response: {0}", resultCreateCharge);

        dynamic? chargeCode = JsonConvert.DeserializeObject(resultCreateCharge) ?? null;

        transactionInfo.Code = chargeCode.codigoSolicitacao.ToString();
        TransactionResponse? tr = await requestTransactionData.ExecuteAsync(transactionInfo);

        return tr;
      }
      else
      {
        Console.WriteLine("Error, received status code {0}: {1}", response.StatusCode, response.ReasonPhrase);
        Console.WriteLine("Retorno: {0}", response.Content.ReadAsStringAsync().Result);
        throw new Exception($"Falha ao gerar a cobrança. {response.Content.ReadAsStringAsync().Result}");
      }
    }
  }
}


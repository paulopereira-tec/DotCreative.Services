using DotCreative.Services.BaaS.Domain.Entities;
using DotCreative.Services.BaaS.Domain.Enums;
using DotCreative.Services.BaaS.Domain.ValuesObject;
using DotCreative.Services.BaaS.Factories.Interfaces;
using DotCreative.Services.BaaS.Factories.Platforms.Abstractions;
using Newtonsoft.Json;
using System.Transactions;

namespace DotCreative.Services.BaaS.Factories.Platforms.Inter;

public class InterRequestBilletDataTransaction : PlatformAbstract, ITransactionRequest
{
  private string _urlBase;

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

  private ETransactionStatus GetStatus(string status)
  => status switch
  {
    "RECEBIDO" => ETransactionStatus.Paid,
    "CANCELADO" => ETransactionStatus.Canceled,
    _ => ETransactionStatus.Waiting
  };

  public async Task<TransactionResponse?> ExecuteAsync(TransactionRequest transactionInfo)
  {
    string uriSufix = GetEndpoint("Inter_Boleto_Arquivo");
    string URI_Cobranca = string.Format($"{_baseUrl}/{uriSufix}", transactionInfo.Code);

    var clientHandler = new HttpClientHandler();
    clientHandler.ClientCertificates.Add(transactionInfo.Platform.Certificate);
    clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;

    using (HttpClient client = new HttpClient(clientHandler))
    {
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Add("Authorization", "Bearer " + transactionInfo.Platform.BearerToken);

      HttpResponseMessage response_detalhe = await client.GetAsync(URI_Cobranca);
      if (response_detalhe.IsSuccessStatusCode)
      {
        string resultRequestCharge = await response_detalhe.Content.ReadAsStringAsync();
        Console.WriteLine("Received response: {0}", resultRequestCharge);

        dynamic? chargeData = JsonConvert.DeserializeObject(resultRequestCharge) ?? null;

        string pdf = await RequestBilletFileAsync(transactionInfo.Code, transactionInfo.Platform);

        return new TransactionResponse
        {
          Billet = new Billet
          {
            BarCode = chargeData.boleto.codigoBarras,
            OurNumber = chargeData.boleto.nossoNumero,
            DigitableLine = chargeData.boleto.linhaDigitavel,
            File = pdf
          },
          Pix = new Pix
          {
            Code = chargeData.pix.pixCopiaECola,
            TxId = chargeData.pix.txid
          },
          Code = transactionInfo.Code,
          TransactionType = ETransactionType.RequestBilletData,
          Status = GetStatus(chargeData.cobranca.situacao.ToString()),
          Amount = chargeData.cobranca.valorNominal
        };
      }
      else
      {
        throw new Exception($"Error, received status code {response_detalhe.StatusCode}: {response_detalhe.ReasonPhrase}");
      }
    }
  }

}
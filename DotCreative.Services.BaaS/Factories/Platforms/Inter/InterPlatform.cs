using DotCreative.Services.BaaS.Domain.Entities;
using DotCreative.Services.BaaS.Domain.Enums;
using DotCreative.Services.BaaS.Factories.Platforms.Abstractions;
using DotCreative.Services.BaaS.Factories.Interfaces;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace DotCreative.Services.BaaS.Factories.Platforms.Inter;

public class InterPlatform : PlatformAbstract, IPlatform
{
  private readonly PlatformRequest _platformDefaultRequest;

  public InterPlatform(PlatformRequest request)
  {
    _platformDefaultRequest = request;

    _baseUrl = request.IsSandbox
      ? GetEndpoint("Inter_BaseUrl_Sandbox")
      : GetEndpoint("Inter_BaseUrl_Production");
  }

  /// <summary>
  /// Autoriza a conexão com a plataforma bancária e devolve as informações de conexão.
  /// </summary>
  public PlatformResponse Authorize()
  {
    HttpClient client;
    string? bearerToken;

    X509Certificate2 cert = _platformDefaultRequest.Certificate;

    //Obtendo bearer token 
    bearerToken = GetToken();

    return new PlatformResponse
    {
      Certificate = cert,
      BearerToken = bearerToken,
      Platform = EPlatform.Inter,
      ClientSecret = _platformDefaultRequest.ClientSecret,
      ClientId = _platformDefaultRequest.ClientId
    };
  }

  /// <summary>
  /// Recupera o token JWT para execução das transações.
  /// </summary>
  private String? GetToken()
  {
    HttpClient client;

    var clientHandlerOauth = new HttpClientHandler();
    clientHandlerOauth.ClientCertificateOptions = ClientCertificateOption.Manual;
    clientHandlerOauth.ClientCertificates.Add(_platformDefaultRequest.Certificate);

    string uriSufix = GetEndpoint("Inter_Autenticacao");
    string URI_Token = $"{_baseUrl}/{uriSufix}";

    var data = new[]
    {
      new KeyValuePair<string, string>("client_id", _platformDefaultRequest.ClientId),
      new KeyValuePair<string, string>("client_secret", _platformDefaultRequest.ClientSecret),
      new KeyValuePair<string, string>("scope", string.Join(" ", _platformDefaultRequest.Scopes)),
      new KeyValuePair<string, string>("grant_type", "client_credentials")
    };

    using (client = new HttpClient(clientHandlerOauth))
    {
      var response = client.PostAsync(URI_Token, new FormUrlEncodedContent(data)).GetAwaiter().GetResult();

      String jsonStr = response.Content.ReadAsStringAsync().Result;

      TokenModel? tokenModel = JsonSerializer.Deserialize<TokenModel>(jsonStr);
      String bearerToken = tokenModel?.access_token;

      client.Dispose();

      return bearerToken;
    }
  }

}

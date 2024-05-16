using DotCreative.Services.BaaS.Domain.Entities;
using DotCreative.Services.BaaS.Domain.Enums;
using DotCreative.Services.BaaS.Factories.Interfaces;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace DotCreative.Services.BaaS.Factories.PlatformFactories.BancoInter;

public class BancoInterPlatform : IPlatform
{
    private readonly PlatformInfo _platformDefaultInfo;

    public BancoInterPlatform(PlatformInfo info)
    {
        _platformDefaultInfo = info;
    }

    public PlatformResult Authorize()
    {
        HttpClient client;
        string? bearerToken;

        X509Certificate2 cert = _platformDefaultInfo.Certificate;

        //Obtendo bearer token 
        bearerToken = GetToken();

        return new PlatformResult
        {
            Certificate = cert,
            BearerToken = bearerToken,
            Platform = EPlatform.Inter,
            ClientSecret = _platformDefaultInfo.ClientSecret,
            ClientId = _platformDefaultInfo.ClientId
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
        clientHandlerOauth.ClientCertificates.Add(_platformDefaultInfo.Certificate);

        String URI_Token = "https://cdpj.partners.bancointer.com.br/oauth/v2/token";

        var data = new[]
        {
            new KeyValuePair<string, string>("client_id", _platformDefaultInfo.ClientId),
            new KeyValuePair<string, string>("client_secret", _platformDefaultInfo.ClientSecret),
            new KeyValuePair<string, string>("scope", string.Join(" ", _platformDefaultInfo.Scopes)),
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

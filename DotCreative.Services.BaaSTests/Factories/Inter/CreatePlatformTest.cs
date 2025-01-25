using DotCreative.Services.BaaS.Domain.Entities;
using DotCreative.Services.BaaS.Domain.Enums;
using DotCreative.Services.BaaS.Factories;
using DotCreative.Services.BaaSTests.Interfaces;
using FluentAssertions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace DotCreative.Services.BaaSTests.Factories.Inter;

[TestFixture]
internal class CreatePlatformTest : IPlatformTest
{
  [Test]
  public void MustBeSuccessOnAuthorization()
  {
    // Arrange
    string directoryBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    string crt = Path.Combine(directoryBase, "BancoInter/Assets/Inter_API_Certificado.crt");
    string key = Path.Combine(directoryBase, "BancoInter/Assets/Inter_API_Chave.key");

    string certPem = File.ReadAllText(crt);
    string keyPem = File.ReadAllText(key);

    /**
     * Gambiarra:
     * No windows, a camada TLS requer que a chave privada seja gravada em disco.
     * O carregamento de certificados baseado em PEM não faz isso, apenas o carregamento de PFX faz.
     * A solução foi encontrada no StackOverflow - Resposta 19.
     * @see https://stackoverflow.com/questions/72096812/loading-x509certificate2-from-pem-file-results-in-no-credentials-are-available
     */
    X509Certificate2 certificate = X509Certificate2.CreateFromPem(certPem, keyPem);
    X509Certificate2 cert = new X509Certificate2(certificate.Export(X509ContentType.Pfx));

    PlatformRequest platform = new PlatformRequest
    {
      Platform = EPlatform.Inter,
      ClientId = "007d526a-8744-490f-a0ce-8d22e353f397",
      ClientSecret = "f47b74b3-b40d-4a81-a8d0-52c36b64188b",
      Scopes = new List<string> { "boleto-cobranca.read", "boleto-cobranca.write" },
      Certificate = cert
    };

    // Act
    PlatformResponse result = new PlatformFactory(platform).PlatformResponse;

    // Assert
    result.Platform.Should().Be(EPlatform.Inter);
  }
}
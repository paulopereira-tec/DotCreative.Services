using DotCreative.Services.BaaS.Domain.Enums;
using System.Security.Cryptography.X509Certificates;

namespace DotCreative.Services.BaaS.Domain.Entities;

/// <summary>
/// Informações padrões e comuns a todas as plataformas.
/// </summary>
public class PlatformRequest
{
  /// <summary>
  /// Define se as transações ocorrerão em ambinete de sandbox ou produção.
  /// </summary>
  public bool IsSandbox { get; set; }

  /// <summary>
  /// Qual plataforma está sendo usada
  /// </summary>
  public EPlatform Platform { get; set; }

  /// <summary>
  /// Código identificador do cliente na plataforma.
  /// </summary>
  public string ClientId { get; set; }

  /// <summary>
  /// Chave secreta de acesso à plataforma.
  /// </summary>
  public string ClientSecret { get; set; }

  /// <summary>
  /// Escopos cadastrados. Exclusivo do Banco Inter.
  /// </summary>
  public IEnumerable<string> Scopes { get; set; }

  /// <summary>
  /// Certificado digital utilizado na conexão. Usado pela maioria dos bancos.
  /// </summary>
  public X509Certificate2? Certificate { get; set; }

}

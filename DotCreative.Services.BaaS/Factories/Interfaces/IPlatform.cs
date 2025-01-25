using DotCreative.Services.BaaS.Domain.Entities;

namespace DotCreative.Services.BaaS.Factories.Interfaces;

/// <summary>
/// Define os parâmetros de conexão e autorização com a plataforma/banco
/// </summary>
public interface IPlatform
{
    /// <summary>
    /// Realiza a autenticação e autorização de acesso para com a plataforma/banco.
    /// </summary>
    /// <returns>Devolve as informações o contexto da contexão.</returns>
    PlatformResponse Authorize();
}

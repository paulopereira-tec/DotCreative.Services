namespace DotCreative.Services.BaaS.Domain.Enums;

public enum ETransactionType : int
{
    /// <summary>
    /// Criação de um boleto.
    /// </summary>
    CreateBillet = 1,

    /// <summary>
    /// Requisição de dados do boleto
    /// </summary>
    RequestBilletData = 2
}

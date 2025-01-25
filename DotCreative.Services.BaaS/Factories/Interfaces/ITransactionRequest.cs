using DotCreative.Services.BaaS.Domain.Entities;

namespace DotCreative.Services.BaaS.Factories.Interfaces;

/// <summary>
/// Define os critérios para a criação de uma transação
/// </summary>
public interface ITransactionRequest
{
    /// <summary>
    /// Executa a solicitação da transação
    /// </summary>
    Task<TransactionResponse?> ExecuteAsync(TransactionRequest transactionInfo);
}

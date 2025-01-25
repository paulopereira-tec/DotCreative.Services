using DotCreative.Services.BaaS.Domain.Entities;
using DotCreative.Services.BaaS.Domain.Enums;
using DotCreative.Services.BaaS.Factories.Interfaces;
using DotCreative.Services.BaaS.Factories.Platforms.Inter;

namespace DotCreative.Services.BaaS.Factories;

/// <summary>
/// Aqui, transação é tudo o que pode ser feito em uma plataforma bancária: pagamentos, criar boletos, consultar extratos, requisitar pagamentos, etc.
/// </summary>
public static class TransactionFactory
{
  /// <summary>
  /// Executa a transação em conformidade com o tipo e plataforma requisitada.
  /// </summary>
  public static async Task<TransactionResponse?> ExecuteAsync(TransactionRequest transactionRequest)
  {
    ITransactionRequest _transactionRequest = transactionRequest.Platform.Platform switch
    {
      EPlatform.Inter => transactionRequest.Type switch
      {
        ETransactionType.CreateBillet => new InterCreateBilletTransaction(),
        ETransactionType.RequestBilletData => new InterRequestBilletDataTransaction(),
        _ => throw new NotImplementedException("Transação não implementada.")
      }
    };

    return await _transactionRequest.ExecuteAsync(transactionRequest);
  }

}

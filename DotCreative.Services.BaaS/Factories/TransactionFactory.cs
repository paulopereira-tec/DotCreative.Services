using DotCreative.Services.BaaS.Domain.Entities;
using DotCreative.Services.BaaS.Domain.Enums;
using DotCreative.Services.BaaS.Factories.Interfaces;
using DotCreative.Services.BaaS.Factories.PlatformFactories.BancoInter;

namespace DotCreative.Services.BaaS.Factories;

public static class TransactionFactory
{

    public static async Task<TransactionResult?> ExecuteAsync(TransactionInfo transactionInfo)
    {
        ITransactionRequest _transactionRequest = transactionInfo.Platform.Platform switch
        {
            EPlatform.Inter => transactionInfo.Type switch
            {
                ETransactionType.CreateBillet => new BancoInterCreateBilletTransaction(),
                ETransactionType.RequestBilletData => new BancoInterRequestBilletDataTransaction(),
                _ => throw new NotImplementedException("Transação não implementada.")
            }
        };

        return await _transactionRequest.ExecuteAsync(transactionInfo);
    }

}

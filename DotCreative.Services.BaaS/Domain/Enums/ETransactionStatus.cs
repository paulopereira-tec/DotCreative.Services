namespace DotCreative.Services.BaaS.Domain.Enums;

public enum ETransactionStatus
{
    /// <summary>
    /// Aguardando pagamento
    /// </summary>
    Waiting = 0,

    /// <summary>
    /// Pago
    /// </summary>
    Paid = 1,

    /// <summary>
    /// Cancelada
    /// </summary>
    Canceled = 2
}

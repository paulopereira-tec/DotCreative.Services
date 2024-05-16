using DotCreative.Services.BaaS.Domain.Enums;
using DotCreative.Services.BaaS.Domain.ValuesObject;

namespace DotCreative.Services.BaaS.Domain.Entities;

public class TransactionResult
{
    public ETransactionType TransactionType { get; set; }
    public Billet Billet { get; set; }
    public Pix Pix { get; set; }
    public string Code { get; set; }
    public decimal Amount { get; set; }
    public ETransactionStatus Status { get; set; }
    public DateOnly Duedate { get; set; }
}

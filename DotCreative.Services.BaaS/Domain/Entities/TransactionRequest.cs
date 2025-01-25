using DotCreative.Services.BaaS.Domain.Enums;
using DotCreative.Services.BaaS.Domain.ValuesObject;

namespace DotCreative.Services.BaaS.Domain.Entities;

public class TransactionRequest
{
  public ETransactionType Type { get; set; }
  public PlatformResponse Platform { get; set; }
  public Person Receiver { get; set; }
  public Person Payer { get; set; }
  public Person? Beneficary { get; set; }
  public Attachment? Discount { get; set; }
  public Attachment? Penalty { get; set; }
  public Messages Messages { get; set; }

  public int DaysOfCancel { get; set; } = 60;
  public decimal Amount { get; set; }
  public DateOnly DueDate { get; set; }
  public string Code { get; set; }

}

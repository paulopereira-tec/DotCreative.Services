using Bogus;
using DotCreative.Services.BaaS.Domain.Entities;
using DotCreative.Services.BaaS.Domain.Enums;
using DotCreative.Services.BaaS.Domain.ValuesObject;

namespace DotCreative.Services.BaaSTests.FakeBuilders;

public class TransactionInfoFakeBuilder
{
    private Attachment _discount { get; set; }
    private Attachment _penalty { get; set; }
    private string Code { get; set; }

    private ETransactionType _transactionType { get; set; }

    public TransactionInfoFakeBuilder WithDiscount(bool applyPercentage)
    {
        _discount = AttachmentFakeBuilder.GenerateOne(applyPercentage, false);
        return this;
    }
    public TransactionInfoFakeBuilder WithPenalty(bool applyPercentage)
    {
        _penalty = AttachmentFakeBuilder.GenerateOne(applyPercentage);
        return this;
    }

    public TransactionInfoFakeBuilder SetType(ETransactionType type)
    {
        _transactionType = type;
        return this;
    }

    public TransactionInfoFakeBuilder SetCode(string code)
    {
        Code = code;
        return this;
    }

    public TransactionRequest Build(PlatformResponse platformResult, bool withBeneficiary = false)
    {
        return new Faker<TransactionRequest>()
            .RuleFor(x => x.Receiver, PersonFakeBuilder.GenerateOne())
            .RuleFor(x => x.Payer, PersonFakeBuilder.GenerateOne())
            .RuleFor(x => x.Beneficary, PersonFakeBuilder.GenerateOne())
            .RuleFor(x => x.Amount, f => f.Random.Decimal(5, 100))
            .RuleFor(x => x.Penalty, _penalty)
            .RuleFor(x => x.Discount, _discount)
            .RuleFor(x => x.Type, _transactionType)
            .RuleFor(x => x.Platform, platformResult)
            .RuleFor(x => x.Messages, MessagesFakeBuilder.Build())
            .RuleFor(x => x.DueDate, f => f.Date.FutureDateOnly(2))
            .RuleFor(x => x.Code, Code)
            ;
    }

    public IEnumerable<TransactionRequest> Build(PlatformResponse platformResult, int quantity, bool withBeneficiary = false)
    {
        for (int i = 0; i < quantity; i++)
        {
            yield return Build(platformResult, withBeneficiary);
        }
    }
}

using Bogus;
using DotCreative.Services.BaaS.Domain.ValuesObject;

namespace DotCreative.Services.BaaSTests.FakeBuilders;

public static class AttachmentFakeBuilder
{
    public static Attachment GenerateOne(bool applyPercentage = false, bool future = true)
    {
        return new Faker<Attachment>()
            .RuleFor(x => x.Date, f => future ? f.Date.FutureDateOnly(0).AddDays(5) : f.Date.PastDateOnly(0).AddDays(5))
            .RuleFor(x => x.ApplyPercentage, applyPercentage)
            .RuleFor(x => x.Amount, f => f.Random.Decimal(10, 2))
            ;
    }
}

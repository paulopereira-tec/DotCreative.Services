using Bogus;
using DotCreative.Services.BaaS.Domain.ValuesObject;

namespace DotCreative.Services.BaaSTests.FakeBuilders;

public static class MessagesFakeBuilder
{
    public static Messages Build()
    {
        return new Faker<Messages>()
            .RuleFor(x => x.One, f => f.Random.Words(5))
            .RuleFor(x => x.Two, f => f.Random.Words(5))
            .RuleFor(x => x.Tree, f => f.Random.Words(5))
            .RuleFor(x => x.Four, f => f.Random.Words(5))
            ;
    }

    public static IEnumerable<Messages> Build(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            yield return Build();
        }
    }
}

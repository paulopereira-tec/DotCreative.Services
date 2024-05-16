using Bogus;
using DotCreative.Services.BaaS.Domain.ValuesObject;

namespace DotCreative.Services.BaaSTests.FakeBuilders;

public class ContactFakeBuilder
{
    public static Contact Build()
    {
        return new Faker<Contact>()
            .RuleFor(x => x.DDD, "32")
            .RuleFor(x => x.Telephone, "7492-8382")
            ;
    }

    public static IEnumerable<Contact> Build(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            yield return Build();
        }
    }
}

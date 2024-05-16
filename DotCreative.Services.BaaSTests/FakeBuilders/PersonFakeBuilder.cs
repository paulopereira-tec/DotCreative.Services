using Bogus;
using Bogus.Extensions.Brazil;

namespace DotCreative.Services.BaaSTests.FakeBuilders;

public class PersonFakeBuilder
{
    public static BaaS.Domain.ValuesObject.Person GenerateOne()
    {
        return new Faker<BaaS.Domain.ValuesObject.Person>()
            .RuleFor(x => x.Name, FakeBuilderAbstract.Brasil.Person.FullName)
            .RuleFor(x => x.Document, FakeBuilderAbstract.Brasil.Person.Cpf())
            .RuleFor(x => x.Personality, BaaS.Domain.Enums.EPersonality.PF)
            .RuleFor(x => x.Address, AddressFakeBuilder.Build())
            .RuleFor(x => x.Contact, ContactFakeBuilder.Build())
            .RuleFor(x => x.Email, f => f.Person.Email)
            ;
    }

    public static IEnumerable<BaaS.Domain.ValuesObject.Person> GenerateMany(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            yield return GenerateOne();
        }
    }
}

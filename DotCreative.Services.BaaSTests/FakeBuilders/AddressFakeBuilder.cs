using Bogus;
using DotCreative.Services.BaaS.Domain.ValuesObject;

namespace DotCreative.Services.BaaSTests.FakeBuilders;

public static class AddressFakeBuilder
{
    public static Address Build()
    {
        return new Faker<Address>()
            .RuleFor(x => x.Street, FakeBuilderAbstract.Brasil.Address.StreetName())
            .RuleFor(x => x.Number, f => f.Random.AlphaNumeric(5))
            .RuleFor(x => x.Complement, "complemento qualquer")
            .RuleFor(x => x.Neighborhood, "bairro qualquer")
            .RuleFor(x => x.City, f => FakeBuilderAbstract.Brasil.Address.City())
            .RuleFor(x => x.State, f => FakeBuilderAbstract.Brasil.Address.StateAbbr())
            .RuleFor(x => x.Zipcode, f => FakeBuilderAbstract.Brasil.Address.ZipCode())
            ;
    }

    public static IEnumerable<Address> Build(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            yield return Build();
        }
    }
}

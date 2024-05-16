using Bogus;

namespace DotCreative.Services.BaaSTests.FakeBuilders;

public abstract class FakeBuilderAbstract
{
    public static Faker Brasil = new Faker("pt_BR");
}

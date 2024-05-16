using DotCreative.Services.BaaS.Domain.Enums;

namespace DotCreative.Services.BaaS.Domain.ValuesObject;

public class Person
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
    public Contact Contact { get; set; }
    public EPersonality Personality { get; set; }
    public string Document { get; set; }
}

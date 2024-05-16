using DotCreative.Services.BaaS.Domain.ExtensionMethods;

namespace DotCreative.Services.BaaS.Domain.ValuesObject;

public class Messages
{
    public string One { get; set; } = string.Empty;
    public string Two { get; set; } = string.Empty;
    public string Tree { get; set; } = string.Empty;
    public string Four { get; set; } = string.Empty;
    public string Five { get; set; } = string.Empty;

    public Messages AddMessage(string message)
    {
        if (One.IsNullOrEmpty())
        {
            One = message;
            return this;
        }

        if (Two.IsNullOrEmpty())
        {
            One = message;
            return this;
        }

        if (Tree.IsNullOrEmpty())
        {
            One = message;
            return this;
        }

        if (Four.IsNullOrEmpty())
        {
            One = message;
            return this;
        }
        return this;
    }
}

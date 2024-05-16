namespace DotCreative.Services.BaaS.Domain.ValuesObject;

/// <summary>
/// Anexos podem ser: descontos, multas, juros, mora
/// </summary>
public class Attachment
{
    /// <summary>
    /// Determina se o valor aplicado será (true) ou não (false) porcentagem sobre o valor principal.
    /// </summary>
    public bool ApplyPercentage { get; set; }

    /// <summary>
    /// Data limite para aplicação
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Valor a ser aplicado
    /// </summary>
    public decimal Amount { get; set; }
}

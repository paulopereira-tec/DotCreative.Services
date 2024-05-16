using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DotCreative.Services.BaaS.Domain.ExtensionMethods;

internal static class StringExtension
{
    /// <summary>
    /// Valida se uma string é nula ou vazia.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string? value)
    => string.IsNullOrEmpty(value.ControlSpaces());

    /// <summary>
    /// Valida se uma string NÃO é nula ou vazia.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNotNullOrEmpty(this string? value)
    => !string.IsNullOrEmpty(value.ControlSpaces());

    /// <summary>
    /// Devolve nulo se o conteúdo da string for vazio ou a string com os espaços controlados.
    /// </summary>
    /// <param name="value">String a ser validada.</param>
    /// <returns></returns>
    public static string? IsNullIfEmpty(this string value)
      => value.ControlSpaces().IsNullOrEmpty() ? null : value.ControlSpaces();

    /// <summary>
    /// Calcula o hash MD5 de uma string
    /// </summary>
    /// <param name="input">String a ser calculada.</param>
    /// <returns></returns>
    public static string GetHash(this string input)
    {
        MD5 md5Hash = MD5.Create();
        // Converter a String para array de bytes, que é como a biblioteca trabalha.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Cria-se um StringBuilder para recompôr a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop para formatar cada byte como uma String em hexadecimal
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();
    }

    public static int ToInt32(this string? value)
    => Convert.ToInt32(value);

    /// <summary>
    /// Controla os espaços entre as palavras, no início e fim das sentenças.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ControlSpaces(this string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        string newString = Regex.Replace(value, @"/\s{2,}/gm", "");
        newString = Regex.Replace(value, @"/$\s{2,}/gm", "");
        newString = Regex.Replace(value, @"/\s{2,}^/gm", "");

        return newString;
    }

    /// <summary>
    /// Captura o valor no formato de uma string enviado e converte-o para um valor decimal.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static decimal ConvertToDecimal(this string value)
    {
        string _valueString = value.Replace("R$ ", "");
        return Convert.ToDecimal(_valueString);
    }

    /// <summary>
    /// Cria um código identificador a partir das iniciais do valor da string
    /// </summary>
    /// <returns></returns>
    public static string CreateCodeBasedOnValue(this string value)
    {
        string[] titleSplited = value.Split(' ');
        StringBuilder code = new();

        foreach (string initialWord in titleSplited)
            code.Append(initialWord.Substring(0, 1));

        return code.ToString().ToUpper();
    }

    /// <summary>
    /// Codifica um texto em base64
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string EncodeToBase64(this string content)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(content);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static string DecodeFromBase64(this string content)
    {
        if (content.IsValidBase64() is false)
            return "";

        var bytesContent = Convert.FromBase64String(content);
        return Encoding.UTF8.GetString(bytesContent);
    }

    public static bool IsValidBase64(this string content)
    {
        content = content.Trim();
        return (content.Length % 4 == 0) && Regex.IsMatch(content, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
    }

    /// <summary>
    /// Valida se o conteúdo de uma string é um GUID válido.
    /// </summary>
    /// <see cref="https://uibakery.io/regex-library/uuid-regex-csharp"/>
    public static bool IsValidGuid(this string guid)
    {
        Regex validateUUIDRegex = new Regex("^[0-9a-f]{8}-[0-9a-f]{4}-[0-5][0-9a-f]{3}-[089ab][0-9a-f]{3}-[0-9a-f]{12}$");
        return validateUUIDRegex.IsMatch(guid);
    }

    /// <summary>
    /// Remove, caracteres especiais e letras deixando apenas números.
    /// </summary>
    public static string OnlyNumbers(this string input)
    {
        // Utiliza expressão regular para substituir todos os caracteres que não sejam números por uma string vazia
        return Regex.Replace(input, @"[^0-9]", "");
    }

    /// <summary>
    /// Quebra uma string até o máximo de caracteres estipulado.
    /// </summary>
    public static string Truncate(this string? value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }
}

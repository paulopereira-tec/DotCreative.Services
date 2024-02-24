namespace DotCreative.Services.Storage.Interfaces;

public interface IObject
{
    /// <summary>
    /// Identificador do objeto armazenado
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Nome do objeto armazenado
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Data de criação
    /// </summary>
    DateTime CreatedAt { get; }

    /// <summary>
    /// Data de atualização, se houver
    /// </summary>
    DateTime UpdatedAt { get; }

    /// <summary>
    /// Descrição, se houver
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Tamanho do objeto, se disponível
    /// </summary>
    long Size { get; }
}

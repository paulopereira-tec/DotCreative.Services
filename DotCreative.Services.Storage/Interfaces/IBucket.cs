namespace DotCreative.Services.Storage.Interfaces;

public interface IBucket
{
    /// <summary>
    /// Identificador do bucket se houver
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Data de criação do bucket
    /// </summary>
    DateTime CreateAt { get; }

    /// <summary>
    /// Data de atualização, se houver.
    /// </summary>
    DateTime UpdateAt { get; }

    /// <summary>
    /// Nome do bucket se houver
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Descrição do bucket se houver
    /// </summary>
    string Description { get; }
}

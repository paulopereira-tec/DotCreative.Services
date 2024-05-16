namespace DotCreative.Services.Storage.Interfaces;

public interface IStorage
{
    /// <summary>
    /// Chave de acesso - equivalente a um login em alguns casos.
    /// </summary>
    string AccessKey { get; }

    /// <summary>
    /// Chave secreta - equivalente a senha em alguns casos.
    /// </summary>
    string SecretKey { get; }

    /// <summary>
    /// URL do serviço a ser conectado.
    /// </summary>
    string ServiceUrl { get; }

    /// <summary>
    /// Recupera uma lista de objetos: arquivos e diretórios.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<IBucket>> GetBucketList();

    /// <summary>
    /// Recupera uma lista de objetos dentro de um bucket
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<IObject>> GetObjectList(IBucket bucket);

    /// <summary>
    /// Baixa um objeto a partir do storage e devolve um array de bytes desse objeto.
    /// </summary>
    Task<byte[]> DownloadObject(IObject obj, IBucket bucket);
}

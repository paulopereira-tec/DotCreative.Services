using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using DotCreative.Services.Storage.Interfaces;

namespace DotCreative.Services.Storage.AmazonS3;

public class AmazonS3Object : IObject
{
    public string Id { get; private set; }

    public string Name { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public string Description { get; private set; }
    public long Size { get; private set; }

    public AmazonS3Object(string id, string name, DateTime createdAt, DateTime updatedAt, string description, long size)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Description = description;
        Size = size;
    }

    public AmazonS3Object(string id, string name, DateTime createdAt, long size)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        Size = size;
    }

    public AmazonS3Object(string id, string name, long size)
    {
        Id = id;
        Name = name;
        Size = size;
    }
}

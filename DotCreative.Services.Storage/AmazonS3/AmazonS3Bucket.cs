using DotCreative.Services.Storage.Interfaces;

namespace DotCreative.Services.Storage.AmazonS3;

public class AmazonS3Bucket : IBucket
{
    public DateTime CreateAt { get; private set; }
    public DateTime UpdateAt { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Id { get; private set; }

    public AmazonS3Bucket(DateTime createAt, DateTime updateAt, string name, string description, string bucketId)
    {
        CreateAt = createAt;
        UpdateAt = updateAt;
        Name = name;
        Description = description;
        Id = bucketId;
    }

    public AmazonS3Bucket(DateTime createAt, string name)
    {
        CreateAt = createAt;
        Name = name;
    }
}

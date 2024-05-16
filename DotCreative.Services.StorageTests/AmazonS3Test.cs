using Amazon.S3.Model;
using DotCreative.Services.Storage.AmazonS3;
using DotCreative.Services.Storage.Interfaces;
using FluentAssertions;

namespace DotCreative.Services.StorageTests;

[TestFixture]
public class AmazonS3Test
{
    private readonly string _accessKey;
    private readonly string _secretKey;
    private readonly string _serviceUrl;

    public AmazonS3Test()
    {
        _accessKey = "Sunhv92VgFmr7ldY";
        _secretKey = "mmXsIKbSGoads7nBya71Nf4g6rOFrJBFvivOtz7w";
        _serviceUrl = "https://s3.tebi.io";
    }

    [Test]
    public async Task EsperaSucessoAoListarOsBuckets()
    {
        // arrange
        IStorage storage = new AmazonS3Storage(_accessKey, _secretKey, _serviceUrl);

        // act
        IEnumerable<IBucket> buckets = await storage.GetBucketList();

        // assert
        buckets.Should().NotBeEmpty();
    }

    [Test]
    public async Task EsperaSucessoAoListarOsObjetosDeUmBucket()
    {
        // arrange
        IStorage storage = new AmazonS3Storage(_accessKey, _secretKey, _serviceUrl);
        IEnumerable<IBucket> buckets = await storage.GetBucketList();
        IBucket bucket = buckets.First();

        // act
        IEnumerable<IObject> objects = await storage.GetObjectList(bucket);

        // assert
        objects.Should().NotBeEmpty();
    }

    [Test]
    public async Task EsperaRecuperarUmArrayDeBytesAoFazerODownloadDoArquivo()
    {
        // arrange
        IStorage storage = new AmazonS3Storage(_accessKey, _secretKey, _serviceUrl);
        IEnumerable<IBucket> buckets = await storage.GetBucketList();
        IBucket bucket = buckets.First();
        IEnumerable<IObject> objects = await storage.GetObjectList(bucket);
        IObject obj = objects.First();

        // act
        byte[] file = await storage.DownloadObject(obj, bucket);

        // assert
        file.Should().NotBeNull();
    }
}

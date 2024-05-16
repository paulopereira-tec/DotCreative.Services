using Amazon.Runtime.Internal;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using DotCreative.Services.Storage.Interfaces;

namespace DotCreative.Services.Storage.AmazonS3;

public class AmazonS3Storage : IStorage
{
    public string AccessKey { get; private set; }
    public string SecretKey { get; private set; }
    public string ServiceUrl { get; private set; }

    public AmazonS3Storage(string accessKey, string secretKey, string serviceUrl)
    {
        AccessKey = accessKey;
        SecretKey = secretKey;
        ServiceUrl = serviceUrl;

        Connect();
    }

    /// <summary>
    /// Client Amazon S3
    /// </summary>
    private AmazonS3Client _client;

    private void Connect()
    {
        /* S3 Config Service URL */
        AmazonS3Config cfg = new AmazonS3Config { ServiceURL = ServiceUrl };

        // This is a workaround for self signed SSL certificates!
        // cfg.HttpClientFactory = new S3SelfSignedFactory.SSLFactory();

        /* S3 Client Configuration (Key, Secret) */
        _client = new AmazonS3Client(awsAccessKeyId: AccessKey, awsSecretAccessKey: SecretKey, clientConfig: cfg);
    }

    public async Task<IEnumerable<IBucket>> GetBucketList()
    {
        List<IBucket> list = new List<IBucket>();

        Amazon.S3.Model.ListBucketsResponse buckets = await _client.ListBucketsAsync();
        foreach (Amazon.S3.Model.S3Bucket bucket in buckets.Buckets)
        {
            list.Add(new AmazonS3Bucket(bucket.CreationDate, bucket.BucketName));
            Console.WriteLine("Bucket: {0}", bucket.BucketName);
        }

        return list;
    }

    public async Task<IEnumerable<IObject>> GetObjectList(IBucket bucket)
    {

        List<IObject> list = new List<IObject>();

        Console.WriteLine("Getting objects... ");
        Amazon.S3.Model.ListObjectsResponse objects = await _client.ListObjectsAsync(bucket.Name);

        foreach (Amazon.S3.Model.S3Object s3Object in objects.S3Objects)
        {
            Console.WriteLine("Bucket {0} -> File: {1}", s3Object.BucketName, s3Object.Key);
            list.Add(new AmazonS3Object(s3Object.Key, s3Object.BucketName, s3Object.LastModified, s3Object.Size));
        }

        return list;
    }

    public async Task<byte[]> DownloadObject (IObject obj, IBucket bucket)
    {
        byte[] bytesContent = default(byte[]);

        GetObjectRequest request = new GetObjectRequest
        {
            BucketName = bucket.Name,
            Key = obj.Id
        };

        using (GetObjectResponse response = await _client.GetObjectAsync(request))
        using (Stream responseStream = response.ResponseStream)
        using (StreamReader reader = new StreamReader(responseStream))
        {
            using (var memstream = new MemoryStream())
            {
                reader.BaseStream.CopyTo(memstream);
                bytesContent = memstream.ToArray();
            }
        }

        return bytesContent;
    }
}

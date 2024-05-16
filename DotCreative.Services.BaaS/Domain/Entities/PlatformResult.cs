using DotCreative.Services.BaaS.Domain.Enums;
using System.Security.Cryptography.X509Certificates;

namespace DotCreative.Services.BaaS.Domain.Entities;

public class PlatformResult
{
    public X509Certificate2 Certificate { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string BearerToken { get; set; }
    public EPlatform Platform { get; set; }
}

using DotCreative.Services.BaaS.Domain.Enums;
using System.Text.Json;

namespace DotCreative.Services.BaaS.Factories.Platforms.Abstractions;

public class PlatformAbstract
{
  private Dictionary<string, string> LoadAllEndpoints()
  {
    string path = Path.Combine(Directory.GetCurrentDirectory(), "endpoints.json");

    if (File.Exists(path))
    {
      string content = File.ReadAllText(path);
      Dictionary<string, string> endpoints = JsonSerializer.Deserialize<Dictionary<string, string>>(content);
      return endpoints;
    }

    return new Dictionary<string, string>();
  }

  protected string _baseUrl { get; set; }

  public string GetEndpoint(string key)
  {
    if (Endpoints.ContainsKey(key))
    {
      return Endpoints[key];
    }
    return string.Empty;
  }

  public Dictionary<string, string> Endpoints { get; private set; }

  public PlatformAbstract()
  {
    Endpoints = LoadAllEndpoints();
  }
}

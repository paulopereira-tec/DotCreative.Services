using DotCreative.Services.BaaS.Domain.Entities;
using DotCreative.Services.BaaS.Domain.Enums;
using DotCreative.Services.BaaS.Factories.Interfaces;
using DotCreative.Services.BaaS.Factories.Platforms.Inter;

namespace DotCreative.Services.BaaS.Factories;

public class PlatformFactory
{
  private readonly IPlatform _platform;

  public PlatformResponse PlatformResponse { get; }

  public PlatformFactory(PlatformRequest platformRequest)
  {
    switch (platformRequest.Platform)
    {
      case EPlatform.Inter: _platform = new InterPlatform(platformRequest); break;
      default: throw new NotImplementedException();
    }

    PlatformResponse = _platform.Authorize();
  }
}

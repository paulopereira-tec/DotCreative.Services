using DotCreative.Services.BaaS.Domain.Entities;
using DotCreative.Services.BaaS.Domain.Enums;
using DotCreative.Services.BaaS.Factories.Interfaces;
using DotCreative.Services.BaaS.Factories.PlatformFactories.BancoInter;

namespace DotCreative.Services.BaaS.Factories;

public class PlatformFactory
{
    private readonly IPlatform _platform;

    public PlatformResult PlatformResult { get; }

    public PlatformFactory(PlatformInfo platformInfo)
    {
        switch (platformInfo.Platform)
        {
            case EPlatform.Inter: _platform = new BancoInterPlatform(platformInfo); break;
            default: throw new NotImplementedException();
        }

        PlatformResult = _platform.Authorize();
    }
}

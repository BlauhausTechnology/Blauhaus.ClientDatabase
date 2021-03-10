using Blauhaus.ClientDatabase.LiteDb.Config;
using Blauhaus.ClientDatabase.LiteDb.Service;
using Blauhaus.Ioc.Abstractions;

namespace Blauhaus.ClientDatabase.LiteDb.Ioc
{
    public static class IocServiceExtensions
    {
        public static IIocService RegisterLiteDb<TConfig>(this IIocService iocService) where TConfig : class, ILiteDbConfig
        {
            iocService.RegisterImplementation<ILiteDbConfig, TConfig>();
            return Register(iocService);
        }

        private static IIocService Register(IIocService iocService)
        {
            iocService.RegisterImplementation<ILiteDbDatabaseService, LiteDbDatabaseService>();
            return iocService;
        }
    }
}
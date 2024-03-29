﻿using Blauhaus.ClientDatabase.Sqlite.Config;
using Blauhaus.ClientDatabase.Sqlite.Service;
using Blauhaus.Ioc.Abstractions;

namespace Blauhaus.ClientDatabase.Sqlite.Ioc
{
    public static class IocRegistration
    {
        public static IIocService RegisterSqlite<TConfig>(this IIocService iocService) where TConfig : class, ISqliteConfig
        {
            iocService.RegisterImplementation<ISqliteConfig, TConfig>();
            return Register(iocService);
        }

        private static IIocService Register(IIocService iocService)
        {
            iocService.RegisterImplementation<ISqliteDatabaseService, SqliteDatabaseService>();
            return iocService;
        }
    }
}
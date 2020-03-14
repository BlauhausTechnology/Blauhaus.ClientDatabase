﻿using Blauhaus.ClientDatabase.Sqlite.Config;
using Blauhaus.ClientDatabase.Sqlite.Service;
using Blauhaus.Ioc.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Blauhaus.ClientDatabase.Sqlite._Ioc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterSqlite<TConfig>(this IServiceCollection services) where TConfig : class, ISqliteConfig
        {
            services.AddScoped<ISqliteConfig, TConfig>();
            return Register(services);
        }

        private static IServiceCollection Register(IServiceCollection services)
        {
            services.AddScoped<ISqliteDatabaseService, SqliteDatabaseService>();
            return services;
        }
    }
}
using System;
using Blauhaus.ClientDatabase.Abstractions;
using Blauhaus.ClientDatabase.Sqlite.Config;
using Blauhaus.ClientDatabase.Sqlite.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Blauhaus.ClientDatabase.Sqlite.Ioc
{
    public static class ServiceCollectionExtensions
    { 
        public static IServiceCollection AddSqlite<TConfig>(this IServiceCollection services) where TConfig : class, ISqliteConfig
        {
            services.AddSingleton<ISqliteConfig, TConfig>();
            return Register(services);
        }

        private static IServiceCollection Register(IServiceCollection services)
        {
            services.AddSingleton<ISqliteDatabaseService, SqliteDatabaseService>();
            services.AddSingleton<IClientDatabaseService>(x => x.GetRequiredService<ISqliteDatabaseService>());
            return services;
        }
    }
}
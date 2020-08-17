using Blauhaus.ClientDatabase.Abstractions;
using Blauhaus.ClientDatabase.Sqlite.Config;
using Blauhaus.ClientDatabase.Sqlite.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            services.TryAddScoped<ISqliteDatabaseService, SqliteDatabaseService>();
            services.TryAddScoped<IClientDatabaseService>(x => x.GetService<ISqliteDatabaseService>());
            return services;
        }
    }
}
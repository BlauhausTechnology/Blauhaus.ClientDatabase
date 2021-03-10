using Blauhaus.ClientDatabase.LiteDb.Config;
using Blauhaus.ClientDatabase.LiteDb.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Blauhaus.ClientDatabase.LiteDb.Ioc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterLiteDb<TConfig>(this IServiceCollection services) where TConfig : class, ILiteDbConfig
        {
            services.AddScoped<ILiteDbConfig, TConfig>();
            return Register(services);
        }

        private static IServiceCollection Register(IServiceCollection services)
        {
            services.AddScoped<ILiteDbDatabaseService, LiteDbDatabaseService>();
            return services;
        }
    }
}
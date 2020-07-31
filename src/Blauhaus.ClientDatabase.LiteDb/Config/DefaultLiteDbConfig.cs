using Blauhaus.ClientDatabase.Abstractions.Config;

namespace Blauhaus.ClientDatabase.LiteDb.Config
{
    public class DefaultLiteDbConfig : DefaultClientDatabaseConfig
    {
        public DefaultLiteDbConfig()
        {
            DatabaseName = "LiteDbDatabase";
        }
    }
}
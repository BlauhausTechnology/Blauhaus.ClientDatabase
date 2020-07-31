namespace Blauhaus.ClientDatabase.Abstractions.Config
{
    public class DefaultClientDatabaseConfig : IClientDatabaseConfig
    {
        public DefaultClientDatabaseConfig()
        {
            DatabaseName = "sqliteDb";
            DatabasePath = string.Empty;
        }

        public string DatabaseName { get; protected set; }
        public string DatabasePath { get; }
    }
}
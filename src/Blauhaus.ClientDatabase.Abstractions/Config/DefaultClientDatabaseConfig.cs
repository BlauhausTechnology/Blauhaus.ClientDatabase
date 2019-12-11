namespace Blauhaus.ClientDatabase.Abstractions.Config
{
    public class DefaultClientDatabaseConfig : IClientDatabaseConfig
    {
        public DefaultClientDatabaseConfig()
        {
            DatabaseName = "LiteDbDatabase";
        }

        public string DatabaseName { get; protected set; }
    }
}
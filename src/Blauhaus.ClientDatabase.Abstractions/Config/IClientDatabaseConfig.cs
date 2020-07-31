namespace Blauhaus.ClientDatabase.Abstractions.Config
{
    public interface IClientDatabaseConfig
    {
        string DatabaseName { get; }
        string DatabasePath { get; }
    }
}
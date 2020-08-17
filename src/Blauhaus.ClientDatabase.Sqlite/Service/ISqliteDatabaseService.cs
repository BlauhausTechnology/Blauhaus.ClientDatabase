using System.Threading.Tasks;
using Blauhaus.ClientDatabase.Abstractions;
using SQLite;

namespace Blauhaus.ClientDatabase.Sqlite.Service
{
    public interface ISqliteDatabaseService : IClientDatabaseService
    {
        ValueTask<SQLiteAsyncConnection> GetDatabaseConnectionAsync();
    }
}
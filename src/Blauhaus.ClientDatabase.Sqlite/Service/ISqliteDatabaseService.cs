using System.Threading.Tasks;
using SQLite;

namespace Blauhaus.ClientDatabase.Sqlite.Service
{
    public interface ISqliteDatabaseService
    {
        ValueTask<SQLiteAsyncConnection> GetDatabaseConnectionAsync();
        Task DropTablesAsync();
    }
}
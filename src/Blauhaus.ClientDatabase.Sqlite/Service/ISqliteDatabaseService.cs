using System.Threading.Tasks;
using SQLite;

namespace Blauhaus.ClientDatabase.Sqlite.Service._Base
{
    public interface ISqliteDatabaseService
    {
        ValueTask<SQLiteAsyncConnection> GetDatabaseConnectionAsync();
    }
}
using Blauhaus.ClientDatabase.Sqlite.Config;
using Blauhaus.ClientDatabase.Sqlite.Service._Base;

namespace Blauhaus.ClientDatabase.Sqlite.Service
{
    public class SqliteInMemoryDatabase : BaseSqliteDatabaseService
    {
        public SqliteInMemoryDatabase(ISqliteConfig config) 
            : base(config, ":memory:") 
        {
        }
         
    }
}
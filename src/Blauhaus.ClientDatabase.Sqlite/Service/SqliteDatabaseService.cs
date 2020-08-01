using System.IO;
using Blauhaus.ClientDatabase.Sqlite.Config;
using Blauhaus.ClientDatabase.Sqlite.Service._Base;

namespace Blauhaus.ClientDatabase.Sqlite.Service
{
    public class SqliteDatabaseService : BaseSqliteDatabaseService
    { 

        public SqliteDatabaseService(ISqliteConfig config) : base(config)
        { 
        }
         

    }
}
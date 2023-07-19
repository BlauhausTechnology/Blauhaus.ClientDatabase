using System.IO;
using Blauhaus.Analytics.Abstractions;
using Blauhaus.ClientDatabase.Sqlite.Config;
using Blauhaus.ClientDatabase.Sqlite.Service.Base;
using Blauhaus.Common.Abstractions;

namespace Blauhaus.ClientDatabase.Sqlite.Service
{
    public class SqliteDatabaseService : BaseSqliteDatabaseService
    {
        public SqliteDatabaseService(
            IAnalyticsLogger<SqliteDatabaseService> logger, 
            IKeyValueStore keyValueStore, 
            ISqliteConfig config) 
                : base(logger, keyValueStore, config, config.DatabasePath)
        {
        }
    }
}
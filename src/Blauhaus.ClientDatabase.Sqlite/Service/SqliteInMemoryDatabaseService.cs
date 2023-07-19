using Blauhaus.Analytics.Abstractions;
using Blauhaus.ClientDatabase.Sqlite.Config;
using Blauhaus.ClientDatabase.Sqlite.Service.Base;
using Blauhaus.Common.Abstractions;

namespace Blauhaus.ClientDatabase.Sqlite.Service
{
    public class SqliteInMemoryDatabaseService : BaseSqliteDatabaseService
    {
        public SqliteInMemoryDatabaseService(
            IAnalyticsLogger<SqliteInMemoryDatabaseService> logger, IKeyValueStore keyValueStore, ISqliteConfig config) 
                : base(logger, keyValueStore, config, ":memory:")
        {
        }
    }
}
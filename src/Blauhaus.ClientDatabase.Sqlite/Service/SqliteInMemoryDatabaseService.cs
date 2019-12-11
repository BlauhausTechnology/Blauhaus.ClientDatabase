using System;
using System.Collections.Generic;
using Blauhaus.ClientDatabase.Sqlite.Service._Base;

namespace Blauhaus.ClientDatabase.Sqlite.Service
{
    public class SqliteInMemoryDatabaseService : BaseSqliteDatabaseService
    {
        public SqliteInMemoryDatabaseService(IList<Type> tableTypes) : base(":memory:", tableTypes)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using Blauhaus.ClientDatabase.Abstractions.Config;

namespace Blauhaus.ClientDatabase.Sqlite.Config
{
    public class DefaultSqliteConfig : DefaultClientDatabaseConfig, ISqliteConfig
    {
        public DefaultSqliteConfig()
        {
            DatabaseName = "SqliteDatabase";
        }

        public IList<Type> TableTypes { get; } = new List<Type>();
    }
}
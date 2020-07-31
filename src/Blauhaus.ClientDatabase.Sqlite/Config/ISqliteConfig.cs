using System;
using System.Collections.Generic;
using Blauhaus.ClientDatabase.Abstractions.Config;

namespace Blauhaus.ClientDatabase.Sqlite.Config
{
    public interface ISqliteConfig : IClientDatabaseConfig
    {
        IList<Type> TableTypes { get; }
        string DatabasePath { get; }
    }
}
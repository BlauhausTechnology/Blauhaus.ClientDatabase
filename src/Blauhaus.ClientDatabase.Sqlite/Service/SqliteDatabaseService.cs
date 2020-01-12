using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blauhaus.ClientDatabase.Sqlite.Config;
using Blauhaus.DeviceServices.Abstractions.DeviceInfo;
using Polly;
using SQLite;

namespace Blauhaus.ClientDatabase.Sqlite.Service
{
    public class SqliteDatabaseService : ISqliteDatabaseService
    {
        private readonly string _connectionString;
        private readonly IList<Type> _tableTypes;
        private SQLiteAsyncConnection? _connection;
        private readonly SQLiteOpenFlags _flags;


        public SqliteDatabaseService(
            ISqliteConfig config,
            IDeviceInfoService deviceInfoService)
        {
            _connectionString = Path.Combine(deviceInfoService.AppDataFolder, config.DatabaseName + ".db");
            _tableTypes = config.TableTypes;
            _flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
        }

        
        public async ValueTask<SQLiteAsyncConnection> GetDatabaseConnectionAsync()
        {
            if (_connection == null)
            {
                _connection = new SQLiteAsyncConnection(_connectionString, _flags);
                await _connection.EnableWriteAheadLoggingAsync().ConfigureAwait(false);
                await _connection.CreateTablesAsync(CreateFlags.None, _tableTypes.ToArray());
            }

            return _connection;

        }

        public async Task DropTablesAsync()
        {
            var connection = await GetDatabaseConnectionAsync();
            foreach (var dbTableMapping in connection.TableMappings)
            {
                await connection.DropTableAsync(dbTableMapping);
            }
        }


        private Task<T> AttemptAndRetry<T>(Func<Task<T>> action, int numRetries = 3)
        {
            return Policy.Handle<SQLiteException>()
                .WaitAndRetryAsync(numRetries, PollyRetryAttempt)
                .ExecuteAsync(action);

            static TimeSpan PollyRetryAttempt(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
        }

    }
}
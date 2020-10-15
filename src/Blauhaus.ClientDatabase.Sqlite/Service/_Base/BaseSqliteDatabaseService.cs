using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blauhaus.ClientDatabase.Sqlite.Config;
using Blauhaus.Responses;
using SQLite;

namespace Blauhaus.ClientDatabase.Sqlite.Service._Base
{
    public abstract class BaseSqliteDatabaseService : ISqliteDatabaseService
    {
        protected string ConnectionString;
        private readonly IList<Type> _tableTypes;
        private SQLiteAsyncConnection? _connection;
        private readonly SQLiteOpenFlags _flags;


        protected BaseSqliteDatabaseService(ISqliteConfig config)
        {

            ConnectionString = config.DatabasePath;

            _tableTypes = config.TableTypes;
            _flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
        }

        
        public async ValueTask<SQLiteAsyncConnection> GetDatabaseConnectionAsync()
        {
            if (_connection == null)
            {
                _connection = new SQLiteAsyncConnection(ConnectionString, _flags);
                await _connection.EnableWriteAheadLoggingAsync().ConfigureAwait(false);
                await _connection.CreateTablesAsync(CreateFlags.None, _tableTypes.ToArray());
            }

            return _connection;

        }

        public async Task ExecuteInTransactionAsync(Action<SQLiteConnection> databaseActions)
        {
            var connection = await GetDatabaseConnectionAsync();
            await connection.RunInTransactionAsync(databaseActions);
        }

        public async Task<T?> ExecuteInTransactionAsync<T>(Func<SQLiteConnection, T> databaseActions) where T : class
        {
            var connection = await GetDatabaseConnectionAsync();
            T? value = default;
            await connection.RunInTransactionAsync(conn =>
            {
                value = databaseActions.Invoke(conn);
            });
            return value;
        }

        public async Task<Response<T>> ExecuteInTransactionAsync<T>(Func<SQLiteConnection, Response<T>> databaseActions) where T : class
        {
            var connection = await GetDatabaseConnectionAsync();
            var result = Response.Failure<T>(Errors.Errors.Undefined);
            await connection.RunInTransactionAsync(conn =>
            {
                result = databaseActions.Invoke(conn);
            });
            return result;
        }

        public async Task DeleteDataAsync()
        {
            var connection = await GetDatabaseConnectionAsync();
            foreach (var dbTableMapping in connection.TableMappings)
            {
                await connection.DropTableAsync(dbTableMapping);
            }

            await _connection.CloseAsync();
            _connection = null;
        }

    }
}
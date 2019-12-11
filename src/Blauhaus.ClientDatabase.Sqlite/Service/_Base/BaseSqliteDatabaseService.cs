using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Polly;
using SQLite;

namespace Blauhaus.ClientDatabase.Sqlite.Service._Base
{
    public abstract class BaseSqliteDatabaseService : ISqliteDatabaseService
    {
        private readonly string _connectionString;
        private readonly IList<Type> _tableTypes;
        private SQLiteAsyncConnection? _connection;
        private readonly SQLiteOpenFlags _flags;
        private string _id;


        protected BaseSqliteDatabaseService(string connectionString, IList<Type> tableTypes)
        {
            _id = Guid.NewGuid().ToString();
            _connectionString = connectionString;
            _tableTypes = tableTypes;
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



        private Task<T> AttemptAndRetry<T>(Func<Task<T>> action, int numRetries = 3)
        {
            return Policy.Handle<SQLiteException>()
                .WaitAndRetryAsync(numRetries, PollyRetryAttempt)
                .ExecuteAsync(action);

            static TimeSpan PollyRetryAttempt(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
        }

    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Blauhaus.ClientDatabase.Sqlite.Config;
using Blauhaus.Responses;
using SQLite;

namespace Blauhaus.ClientDatabase.Sqlite.Service._Base
{
    public abstract class BaseSqliteDatabaseService : ISqliteDatabaseService
    {
        private readonly Type[] _tableTypes;
        private readonly TaskCompletionSource<bool> _initializationTask;

        protected BaseSqliteDatabaseService(ISqliteConfig config, string connectionString)
        {
            _tableTypes = config.TableTypes.ToArray();

             var connection = new SQLiteAsyncConnection(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

             _initializationTask = new TaskCompletionSource<bool>();
             Task.Run(async () =>
             {
                 try
                 {
                     await connection.EnableWriteAheadLoggingAsync();
                     await connection.CreateTablesAsync(CreateFlags.None, _tableTypes);
                     _initializationTask.SetResult(true);
                 }
                 catch (Exception e)
                 {
                     _initializationTask.SetException(e);
                 }
             });

             AsyncConnection = connection;
        }


        public SQLiteAsyncConnection AsyncConnection { get; }

        public Task ExecuteInTransactionAsync(Action<SQLiteConnection> databaseActions)
        {
            return AsyncConnection.RunInTransactionAsync(databaseActions);
        }

        public async Task<T?> ExecuteInTransactionAsync<T>(Func<SQLiteConnection, T> databaseActions) where T : class
        {
            T? value = default;
            await AsyncConnection.RunInTransactionAsync(conn =>
            {
                value = databaseActions.Invoke(conn);
            });
            return value;
        }
         

        public async Task<Response<T>> ExecuteInTransactionAsync<T>(Func<SQLiteConnection, Response<T>> databaseActions) where T : class
        {
            var result = Response.Failure<T>(Errors.Errors.Undefined);
            await AsyncConnection.RunInTransactionAsync(conn =>
            {
                result = databaseActions.Invoke(conn);
            });
            return result;
        }
         
        
        public async Task DeleteDataAsync()
        {
            //ensure initialization is complete
            await _initializationTask.Task;
            
            foreach (var tableType in _tableTypes)
            {
                var tableMap = AsyncConnection.TableMappings.FirstOrDefault(x => x.TableName == tableType.Name);
                if (tableMap != null)
                {
                    await AsyncConnection.DropTableAsync(tableMap); 
                }
            }
            await AsyncConnection.CreateTablesAsync(CreateFlags.None, _tableTypes.ToArray());
        } 
    }
}
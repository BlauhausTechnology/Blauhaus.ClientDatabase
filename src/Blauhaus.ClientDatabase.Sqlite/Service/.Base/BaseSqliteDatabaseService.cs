using System;
using System.Linq;
using System.Threading.Tasks;
using Blauhaus.Analytics.Abstractions;
using Blauhaus.ClientDatabase.Sqlite.Config;
using Blauhaus.Common.Abstractions;
using Blauhaus.Errors;
using Blauhaus.Responses;
using Microsoft.Extensions.Logging;
using SQLite;

namespace Blauhaus.ClientDatabase.Sqlite.Service.Base
{
    public abstract class BaseSqliteDatabaseService : ISqliteDatabaseService
    {
        private readonly Type[] _tableTypes;
        private readonly Task _initializationTask;
        private const string SchemaVersionKey = "SchemaVersion";

        protected BaseSqliteDatabaseService(
            IAnalyticsLogger logger,
            IKeyValueStore keyValueStore,
            ISqliteConfig config, string connectionString)
        {
            _tableTypes = config.TableTypes.ToArray();

             var connection = new SQLiteAsyncConnection(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

             _initializationTask = Task.Run(async () =>
             {
                 try
                 {
                     logger.LogInformation("Initializing sqlite database for connection {SqliteConnectionString}", connectionString);

                     await connection.EnableWriteAheadLoggingAsync();
                     await connection.CreateTablesAsync(CreateFlags.None, _tableTypes);
                     string currentSchemaVersionString = await keyValueStore.GetAsync(SchemaVersionKey);
                     if (!string.IsNullOrEmpty(currentSchemaVersionString) && int.TryParse(currentSchemaVersionString, out int currentSchemaVersion))
                     {
                         if (config.SchemaVersion > currentSchemaVersion)
                         {
                             logger.LogInformation("Schema version changed from {OldSchemaVersion} to {NewSchemaVersion}", currentSchemaVersion, config.SchemaVersion);
                             foreach (var tableType in _tableTypes)
                             {
                                 logger.LogInformation("Deleting table {TableName}", tableType.Name);
                                 await connection.DeleteAllAsync(new TableMapping(tableType));
                             }
                         }
                     }
                 }
                 catch (Exception e)
                 {
                     logger.LogError(e, "Failed to start Sqlite database for connection {SqliteConnectionString}", connectionString);
                     throw;
                 }
             });

             AsyncConnection = connection;
        }


        public async Task EnsureCreatedAsync()
        {
            await _initializationTask;
        }

        public SQLiteAsyncConnection AsyncConnection { get; }

        public async ValueTask<SQLiteAsyncConnection> GetConnectionAsync()
        {
            await _initializationTask;
            return AsyncConnection;
        }

        public async Task ExecuteInTransactionAsync(Action<SQLiteConnection> databaseActions)
        {
            await _initializationTask;
            await AsyncConnection.RunInTransactionAsync(databaseActions);
        }

        public async Task<T?> ExecuteInTransactionAsync<T>(Func<SQLiteConnection, T> databaseActions) where T : class
        {
            await _initializationTask;
            
            T? value = default;
            await AsyncConnection.RunInTransactionAsync(conn =>
            {
                value = databaseActions.Invoke(conn);
            });
            return value;
        }
         

        public async Task<Response<T>> ExecuteInTransactionAsync<T>(Func<SQLiteConnection, Response<T>> databaseActions) where T : class
        {
            await _initializationTask;
            
            var result = Response.Failure<T>(Error.Undefined);
            await AsyncConnection.RunInTransactionAsync(conn =>
            {
                result = databaseActions.Invoke(conn);
            });
            return result;
        }
         
        
        public async Task DeleteDataAsync()
        {
            await _initializationTask;
            
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
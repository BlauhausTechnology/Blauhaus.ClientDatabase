using System;
using System.Threading.Tasks;
using Blauhaus.ClientDatabase.Abstractions;
using Blauhaus.Responses;
using SQLite;

namespace Blauhaus.ClientDatabase.Sqlite.Service
{
    public interface ISqliteDatabaseService : IClientDatabaseService
    {
        Task EnsureCreatedAsync();
        SQLiteAsyncConnection AsyncConnection { get; }
        ValueTask<SQLiteAsyncConnection> GetConnectionAsync();

        Task ExecuteInTransactionAsync(Action<SQLiteConnection> databaseActions);
        Task<T?> ExecuteInTransactionAsync<T>(Func<SQLiteConnection, T> databaseActions) where T : class;
        Task<Response<T>> ExecuteInTransactionAsync<T>(Func<SQLiteConnection, Response<T>> databaseActions) where T : class;
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LiteDB;

namespace Blauhaus.ClientDatabase.LiteDb.Service.Base
{
    public interface ILiteDbDatabaseService
    {
        LiteDatabase GetDatabase();
        void EnsureIndex<T, TProperty>(BsonExpression expression);

        void Insert<T>(IEnumerable<T> entities);
        void Upsert<T>(IEnumerable<T> entities);
        void Upsert<T>(T entity);

        T Load<T>(Guid id);
        T FindOne<T>(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate, int skip = 0, int limit = 2147483647);

        void Delete<T>(Guid id);
        void WipeAll();
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blauhaus.ClientDatabase.LiteDb.Config;
using LiteDB;

namespace Blauhaus.ClientDatabase.LiteDb.Service.Base
{
    public abstract class BaseLiteDbDatabaseService : ILiteDbDatabaseService
    {
        protected readonly ILiteDbConfig Config;

        protected BaseLiteDbDatabaseService(ILiteDbConfig config)
        {
            Config = config;
        }

        public abstract LiteDatabase GetDatabase();

        public void EnsureIndex<T, TProperty>(BsonExpression expression)
        {
            using (var db = GetDatabase())
            {
                var collection = db.GetCollection<T>();
                collection.EnsureIndex(expression);
            }
        }

        public T Load<T>(Guid id)
        {
            using (var db = GetDatabase())
            {
                var collection = db.GetCollection<T>();
                return collection.FindById(id);
            }
        }

        public void Upsert<T>(IEnumerable<T> entities)
        {
            using (var db = GetDatabase())
            {
                var collection = db.GetCollection<T>();
                foreach (var entity in entities)
                {
                    collection.Upsert(entity);
                }
            }
        }

        public void Upsert<T>(T entity)
        {
            using (var db = GetDatabase())
            {
                var collection = db.GetCollection<T>();
                collection.Upsert(entity);
            }
        }

        public void Insert<T>(IEnumerable<T> entities)
        {
            using (var db = GetDatabase())
            {
                var collection = db.GetCollection<T>();
                collection.Insert(entities);
            }
        }

        public T FindOne<T>(Expression<Func<T, bool>> predicate)
        {
            using (var db = GetDatabase())
            {
                var collection = db.GetCollection<T>();
                return collection.FindOne(predicate);
            }
        }

        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate, int skip = 0, int limit = 2147483647)
        {
            using (var db = GetDatabase())
            {
                var collection = db.GetCollection<T>();
                return collection.Find(predicate, skip, limit);
            }
        }

        
        public void Delete<T>(Guid id)
        {
            using (var db = GetDatabase())
            {
                var collection = db.GetCollection<T>();
                collection.Delete(id);
            }
        }

        public void WipeAll()
        {
            using (var db = GetDatabase())
            {
                foreach (var collectionName in db.GetCollectionNames())
                {
                    db.DropCollection(collectionName);
                }
            }
        }
    }
}
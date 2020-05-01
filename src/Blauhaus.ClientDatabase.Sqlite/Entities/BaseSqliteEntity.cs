using System;
using Blauhaus.Domain.Common.Entities;
using SQLite;

namespace Blauhaus.ClientDatabase.Sqlite.Entities
{
    public abstract class BaseSqliteEntity : IClientEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        
        [Indexed]
        public EntityState EntityState { get; set;}
        
        [Indexed]
        public long ModifiedAtTicks { get; set;}
    }
}
using System;
using Blauhaus.Common.Domain.Entities;
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
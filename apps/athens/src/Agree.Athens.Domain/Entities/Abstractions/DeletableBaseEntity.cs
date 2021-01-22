using System;

namespace Agree.Athens.Domain.Entities.Abstractions
{
    public abstract class DeletableBaseEntity<TId> : BaseEntity<TId>
    {
        public DateTime? DeletedAt { get; set; }
    }
}
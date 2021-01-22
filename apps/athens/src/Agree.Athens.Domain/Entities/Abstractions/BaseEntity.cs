using System;

namespace Agree.Athens.Domain.Entities.Abstractions
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }
        public string IdStr { get => Id.ToString(); }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
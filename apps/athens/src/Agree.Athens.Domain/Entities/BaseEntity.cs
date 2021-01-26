using System;

namespace Agree.Athens.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public string IdStr { get => Id.ToString(); }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
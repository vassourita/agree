using System.Collections.Generic;
using System;
using Agree.Athens.Domain.Entities;

namespace Agree.Athens.Domain.Exceptions
{
    public class EntityNotFoundException<T> : BaseDomainException
        where T : BaseEntity
    {
        public IEnumerable<Guid> NotFoundIds { get; private set; }
        public Type EntityType { get; private set; }

        public EntityNotFoundException(Guid id) : base($"Entity of type ({typeof(T).Name}) with Id ({id}) not found")
        {
            NotFoundIds = new[] { id };
            EntityType = typeof(T);
        }

        public EntityNotFoundException()
            : base($"Entities of type ({typeof(T).Name}) not found")
        {
            NotFoundIds = new Guid[] { };
            EntityType = typeof(T);
        }

        public EntityNotFoundException(IEnumerable<Guid> ids)
            : base($"Entities of type ({typeof(T).Name}) with Ids ({string.Join(", ", ids)}) not found")
        {
            NotFoundIds = ids;
            EntityType = typeof(T);
        }
    }
}
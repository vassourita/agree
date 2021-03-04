using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Exceptions
{
    public class EntityNotFoundException : BaseDomainException
    {
        public EntityNotFoundException(Entity entity) : base($"{entity.ToString()} not found")
        {
        }
    }
}
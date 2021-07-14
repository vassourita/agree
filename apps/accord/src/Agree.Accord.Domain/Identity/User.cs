using Agree.Accord.SharedKernel;

namespace Agree.Accord.Domain.Identity
{
    public class User : Entity
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public DiscriminatorTag Tag { get; private set; }
    }
}
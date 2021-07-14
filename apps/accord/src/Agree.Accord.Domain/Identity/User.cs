using Agree.Accord.SharedKernel;

namespace Agree.Accord.Domain.Identity
{
    public class User : Entity
    {
        public User(string userName, string email, DiscriminatorTag tag)
        {
            UserName = userName;
            Email = email;
            Tag = tag;
        }

        public string UserName { get; private set; }
        public string Email { get; private set; }
        public DiscriminatorTag Tag { get; private set; }
    }
}
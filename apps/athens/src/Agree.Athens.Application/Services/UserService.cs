using System.Threading.Tasks;
using Agree.Athens.Domain.Entities;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Domain.Specifications;
using Agree.Athens.Domain.ValueObjects;

namespace Agree.Athens.Application.Services
{
    public class UserService
    {
        private readonly IBaseRepository<User> _userRepository;

        public UserService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> IsUniqueEmail(string email)
        {
            var usersWithSameEmail = await _userRepository.ListAsync(new UserEmailSpecification(email));
            return usersWithSameEmail.Count > 0;
        }

        public async Task<UserTag> GenerateUniqueTagForUser(string username)
        {
            var randomTag = UserTag.GenerateRandomTag();
            var usersWithSameTag = await _userRepository.ListAsync(new UserTagSpecification(randomTag, username));
            var tagAlreadyInUse = usersWithSameTag.Count > 0;
            while (tagAlreadyInUse)
            {
                randomTag = UserTag.GenerateRandomTag();
                usersWithSameTag = await _userRepository.ListAsync(new UserTagSpecification(randomTag, username));
                tagAlreadyInUse = usersWithSameTag.Count > 0;
            }
            return randomTag;
        }
    }
}
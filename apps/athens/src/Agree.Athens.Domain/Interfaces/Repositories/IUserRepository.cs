using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Entities;

namespace Agree.Athens.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User, Guid>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUsertagAsync(string usertag);
        Task<User> GetByUsertagAsync(string username, int tag);
    }
}
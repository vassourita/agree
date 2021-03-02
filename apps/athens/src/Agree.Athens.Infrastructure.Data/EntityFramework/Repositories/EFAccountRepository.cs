using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Agree.Athens.Infrastructure.Data.EntityFramework.DataModels;
using Agree.Athens.SharedKernel.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Repositories
{
    public class EFAccountRepository : EFBaseRepository<UserAccount, UserDbModel>, IAccountRepository
    {
        public EFAccountRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<bool> EmailIsInUseAsync(string email)
        {
            var item = await _dataSet.SingleOrDefaultAsync(x => x.Email.Equals(email));
            return item != null;
        }

        public async Task<bool> TagIsInUseAsync(UserTag tag, string userName)
        {
            var item = await _dataSet.SingleOrDefaultAsync(x => x.Tag.Equals(tag) && x.UserName == userName);
            return item != null;
        }

        public async Task<UserAccount> SoftDeleteAsync(Guid id)
        {
            var item = await _dataSet.SingleOrDefaultAsync(x => x.Id.Equals(id));

            item.DeletedAt = DateTime.UtcNow;

            var entity = _mapper.Map<UserAccount>(item);

            await UpdateAsync(entity);

            return entity;
        }

        public async Task<UserAccount> SoftDeleteAsync(UserAccount entity)
        {
            var item = await _dataSet.SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));

            entity.SoftDelete();
            var updatedItem = _mapper.Map<UserDbModel>(entity);

            _context.Entry(item).CurrentValues.SetValues(updatedItem);

            return entity;
        }

        public async Task<UserAccount> GetByEmailAsync(string email)
        {
            var item = await _dataSet.SingleOrDefaultAsync(x => x.Email.Equals(email));
            return _mapper.Map<UserAccount>(item);
        }
    }
}
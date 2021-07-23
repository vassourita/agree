using System;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Dtos;
using Agree.Accord.Domain.Identity.Results;
using Agree.Accord.Domain.Identity.Specifications;
using Agree.Accord.Domain.Providers;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using Microsoft.AspNetCore.Identity;

namespace Agree.Accord.Domain.Identity.Services
{
    /// <summary>
    /// Provides methods for managing user accounts.
    /// </summary>
    public class AccountService
    {
        private readonly IRepository<ApplicationUser> _accountRepository;

        public AccountService(IRepository<ApplicationUser> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<DiscriminatorTag> GenerateDiscriminatorTagAsync(string displayName)
        {
            var tag = DiscriminatorTag.NewTag();
            var tagIsInUse = (await _accountRepository.GetFirstAsync(new NameTagEqualSpecification(tag, displayName)) != null);
            while (tagIsInUse)
            {
                tag = DiscriminatorTag.NewTag();
                tagIsInUse = (await _accountRepository.GetFirstAsync(new NameTagEqualSpecification(tag, displayName)) != null);
            }
            return tag;
        }

        public async Task<ApplicationUser> GetAccountByIdAsync(Guid id)
            => await _accountRepository.GetFirstAsync(new IdEqualSpecification(id));

        public async Task<ApplicationUser> GetAccountByEmailAsync(string email)
            => await _accountRepository.GetFirstAsync(new EmailEqualSpecification(email));
    }
}
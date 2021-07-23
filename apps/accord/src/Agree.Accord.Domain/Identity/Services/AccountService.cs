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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRepository<ApplicationUser> _accountRepository;
        private readonly TokenService _tokenService;
        private readonly IMailProvider _mailProvider;

        public AccountService(
            IRepository<ApplicationUser> accountRepository,
            TokenService tokenService,
            IMailProvider mailProvider)
        {
            _accountRepository = accountRepository;
            _tokenService = tokenService;
            _mailProvider = mailProvider;
        }

        /// <summary>
        /// Creates a new user account with the specified data, hashes the password and saves it to the repository.
        /// </summary>
        /// <param name="createAccountDto">The user data used to create the account.</param>
        /// <returns>A result with either the new user account if it succeeded or the validation errors if it has failed.</returns>
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
    }
}
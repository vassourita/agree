using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Dtos;
using Agree.Accord.Domain.Identity.Results;
using Agree.Accord.Domain.Identity.Specifications;
using Agree.Accord.Domain.Providers;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Identity.Services
{
    /// <summary>
    /// Provides methods for managing user accounts.
    /// </summary>
    public class AccountService
    {
        private readonly IHashProvider _hashProvider;
        private readonly IRepository<UserAccount> _accountRepository;

        public AccountService(IHashProvider hashProvider, IRepository<UserAccount> accountRepository)
        {
            _hashProvider = hashProvider;
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Creates a new user account with the specified data, hashes the password and saves it to the repository.
        /// <para>
        /// 
        /// </para>
        /// </summary>
        /// <param name="createAccountDto">The user data used to create the account.</param>
        /// <returns>A result with either the new user account if it succeeded or the validation errors if it has failed.</returns>
        public async Task<CreateAccountResult> CreateAccountAsync(CreateAccountDto createAccountDto)
        {
            var validationResult = AnnotationValidator.TryValidate(createAccountDto);

            var emailIsInUse = (await _accountRepository.GetFirstAsync(new EmailEqualSpecification(createAccountDto.Email)) != null);

            if (emailIsInUse)
            {
                return CreateAccountResult.Fail(validationResult.Error.ToErrorList().AddError("Email", "Email address is already in use"));
            }

            if (validationResult.Failed)
            {
                return CreateAccountResult.Fail(validationResult.Error.ToErrorList());
            }

            var passwordHash = await _hashProvider.HashAsync(createAccountDto.Password);

            var tag = DiscriminatorTag.NewTag();
            var tagIsInUse = (await _accountRepository.GetFirstAsync(new NameTagEqualSpecification(tag, createAccountDto.UserName)) != null);
            while (tagIsInUse)
            {
                tag = DiscriminatorTag.NewTag();
            }

            var account = new UserAccount(createAccountDto.UserName, createAccountDto.Email, passwordHash, tag);

            await _accountRepository.InsertAsync(account);

            return CreateAccountResult.Ok(account);
        }
    }
}
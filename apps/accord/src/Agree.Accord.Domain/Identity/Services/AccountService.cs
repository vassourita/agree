using System;
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
        private readonly TokenService _tokenService;

        public AccountService(IHashProvider hashProvider,
                              IRepository<UserAccount> accountRepository,
                              TokenService tokenService)
        {
            _hashProvider = hashProvider;
            _accountRepository = accountRepository;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Creates a new user account with the specified data, hashes the password and saves it to the repository.
        /// </summary>
        /// <param name="createAccountDto">The user data used to create the account.</param>
        /// <returns>A result with either the new user account if it succeeded or the validation errors if it has failed.</returns>
        public async Task<CreateAccountResult> CreateAccountAsync(CreateAccountDto createAccountDto)
        {
            var validationResult = AnnotationValidator.TryValidate(createAccountDto);

            var emailIsInUse = (await _accountRepository.GetFirstAsync(new EmailEqualSpecification(createAccountDto.Email)) != null);

            if (validationResult.Failed)
            {
                if (emailIsInUse)
                {
                    return CreateAccountResult.Fail(validationResult.Error.ToErrorList().AddError("Email", "Email address is already in use"));
                }
                return CreateAccountResult.Fail(validationResult.Error.ToErrorList());
            }

            if (emailIsInUse)
            {
                return CreateAccountResult.Fail(new ErrorList().AddError("Email", "Email address is already in use"));
            }

            var passwordHash = await _hashProvider.HashAsync(createAccountDto.Password);

            var tag = DiscriminatorTag.NewTag();
            var tagIsInUse = (await _accountRepository.GetFirstAsync(new NameTagEqualSpecification(tag, createAccountDto.UserName)) != null);
            while (tagIsInUse)
            {
                tag = DiscriminatorTag.NewTag();
                tagIsInUse = (await _accountRepository.GetFirstAsync(new NameTagEqualSpecification(tag, createAccountDto.UserName)) != null);
            }

            var account = new UserAccount(createAccountDto.UserName, createAccountDto.Email, passwordHash, tag);

            await _accountRepository.InsertAsync(account);

            await _accountRepository.CommitAsync();

            return CreateAccountResult.Ok(account);
        }

        public async Task<UserAccount> GetAccountByIdAsync(Guid id)
            => await _accountRepository.GetFirstAsync(new IdEqualSpecification(id));

        public async Task<LoginResult> LoginAsync(LoginDto loginDto)
        {
            var account = await _accountRepository.GetFirstAsync(new EmailEqualSpecification(loginDto.Email));
            if (account == null)
            {
                return LoginResult.Fail();
            }
            var passwordsMatch = await _hashProvider.CompareAsync(account.PasswordHash, loginDto.Password);
            if (!passwordsMatch)
            {
                return LoginResult.Fail();
            }
            var token = await _tokenService.GenerateAccessTokenAsync(account);
            return LoginResult.Ok(token);
        }
    }
}
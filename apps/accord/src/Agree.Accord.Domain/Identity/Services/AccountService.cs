using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Dtos;
using Agree.Accord.Domain.Identity.Results;
using Agree.Accord.Domain.Providers;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Identity.Services
{
    public class AccountService
    {
        private readonly IHashProvider _hashProvider;

        public AccountService(IHashProvider hashProvider)
        {
            _hashProvider = hashProvider;
        }

        public async Task<CreateAccountResult> CreateAccountAsync(CreateAccountDto createAccountDto)
        {
            var validationResult = AnnotationValidator.TryValidate(createAccountDto);

            if (validationResult.Failed)
            {
                return CreateAccountResult.Fail(validationResult.Error);
            }

            var passwordHash = await _hashProvider.HashAsync(createAccountDto.Password);

            var tag = DiscriminatorTag.NewTag();

            var account = new UserAccount(createAccountDto.UserName, createAccountDto.Email, passwordHash, tag);

            return CreateAccountResult.Ok(account);
        }
    }
}
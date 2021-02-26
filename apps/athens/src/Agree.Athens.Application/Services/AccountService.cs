using System;
using System.Threading.Tasks;
using System.Web;
using Agree.Athens.Application.Dtos;
using Agree.Athens.Application.Dtos.Validators;
using Agree.Athens.Domain.Services;

namespace Agree.Athens.Application.Services
{
    public class AccountService
    {
        private readonly UserAccountService _userAccountService;
        private readonly MailService _mailService;

        public AccountService(UserAccountService userAccountService, MailService mailService)
        {
            _userAccountService = userAccountService;
            _mailService = mailService;
        }

        public async Task Register(CreateAccountDto createAccountDto, string confirmationUrl)
        {
            var validationResult = new CreateAccountDtoValidator().Validate(createAccountDto);
            if (!validationResult.IsValid)
            {

            }

            var account = await _userAccountService.Register(createAccountDto.UserName, createAccountDto.Email, createAccountDto.Password);

            var confirmationUrlWithToken = AddTokenToMailConfirmationUrl(confirmationUrl, account.Id);

            await _mailService.SendAccountConfirmationMailAsync(account, confirmationUrlWithToken);
        }

        private string AddTokenToMailConfirmationUrl(string confirmationUrl, Guid accountId)
        {
            var uriBuilder = new UriBuilder(confirmationUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = accountId.ToString();
            uriBuilder.Query = query.ToString();
            var confirmationUrlWithToken = uriBuilder.ToString();
            return confirmationUrlWithToken;
        }

        public Task ConfirmEmail(Guid id)
        {
            return _userAccountService.ConfirmEmail(id);
        }
    }
}
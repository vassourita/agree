using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web;
using Agree.Athens.Application.Dtos;
using Agree.Athens.Application.Dtos.Validators;
using Agree.Athens.Application.Security;
using Agree.Athens.Application.ViewModels;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Domain.Services;

namespace Agree.Athens.Application.Services
{
    public class AccountService
    {
        private readonly UserAccountService _userAccountService;
        private readonly MailService _mailService;
        private readonly TokenService _tokenService;
        private readonly ITokenRepository _tokenRepository;

        public AccountService(UserAccountService userAccountService, MailService mailService, TokenService tokenService, ITokenRepository tokenRepository)
        {
            _userAccountService = userAccountService;
            _mailService = mailService;
            _tokenService = tokenService;
            _tokenRepository = tokenRepository;
        }

        public async Task Register(CreateAccountDto createAccountDto, string confirmationUrl)
        {
            createAccountDto.Validate(createAccountDto, new CreateAccountDtoValidator());
            if (createAccountDto.IsInvalid)
            {
                throw new DomainValidationException(createAccountDto);
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

        public async Task<(AccessToken, RefreshToken)> Login(LoginDto loginDto)
        {
            var account = await _userAccountService.Login(loginDto.Email, loginDto.Password);
            var accessToken = _tokenService.GenerateAccessToken(account);
            var refreshToken = _tokenService.GenerateRefreshToken(loginDto.IpAddress, account.Id);
            await _tokenRepository.AddAsync(refreshToken);
            return (accessToken, refreshToken);
        }

        public async Task<(AccessToken, RefreshToken)> RefreshTokens(LoginDto loginDto)
        {
            return await _tokenService.RefreshTokens(loginDto.RefreshToken, loginDto.IpAddress);
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web;
using Agree.Athens.Domain.Dtos;
using Agree.Athens.Domain.Dtos.Validators;
using Agree.Athens.Application.Security;
using Agree.Athens.Application.ViewModels;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Aggregates.Account.Factories;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Domain.Services;
using AutoMapper;
using Agree.Athens.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Agree.Athens.Application.Services
{
    public class AccountService
    {
        private readonly UserAccountService _userAccountService;
        private readonly MailService _mailService;
        private readonly TokenService _tokenService;
        private readonly ITokenRepository _tokenRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly UIConfiguration _uiConfiguration;

        public AccountService(UserAccountService userAccountService,
                              MailService mailService,
                              TokenService tokenService,
                              ITokenRepository tokenRepository,
                              IAccountRepository accountRepository,
                              IMapper mapper,
                              IOptions<UIConfiguration> uiConfiguration)
        {
            _userAccountService = userAccountService;
            _mailService = mailService;
            _tokenService = tokenService;
            _tokenRepository = tokenRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _uiConfiguration = uiConfiguration.Value;
        }

        public async Task<UserAccount> GetUserById(Guid id)
        {
            var account = await _accountRepository.GetByIdAsync(id);

            if (account.DeletedAt is not null)
            {
                return null;
            }

            return account;
        }

        public async Task Register(CreateAccountDto createAccountDto, string confirmationUrl)
        {
            var account = await _userAccountService.Register(createAccountDto);

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

        public async Task<string> ConfirmEmail(Guid id)
        {
            var email = await _userAccountService.ConfirmEmail(id);

            return $"{_uiConfiguration.EmailConfirmRedirectURL}?email_verified={email}";
        }

        public async Task<(AccessToken, RefreshToken)> Login(LoginDto loginDto)
        {
            if (loginDto.GrantType == "refresh_token")
            {
                var (accessToken, refreshToken) = await RefreshTokens(loginDto);
                return (accessToken, refreshToken);
            }
            else
            {
                var account = await _userAccountService.Login(loginDto);
                var accessToken = _tokenService.GenerateAccessToken(account);
                var refreshToken = _tokenService.GenerateRefreshToken(loginDto.IpAddress, account.Id);
                await _tokenRepository.AddAsync(refreshToken);
                return (accessToken, refreshToken);
            }
        }

        public async Task<(AccessToken, RefreshToken)> RefreshTokens(LoginDto loginDto)
        {
            return await _tokenService.RefreshTokens(loginDto.RefreshToken, loginDto.IpAddress);
        }

        public async Task<AccountViewModel> UpdateAccount(UpdateAccountDto updateAccountDto)
        {
            var account = await _userAccountService.Update(updateAccountDto);

            var accountModel = _mapper.Map<AccountViewModel>(account);

            return accountModel;
        }
    }
}
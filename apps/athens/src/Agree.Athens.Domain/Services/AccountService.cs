using System.Collections.Generic;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Domain.Interfaces.Providers;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Domain.Interfaces.Services;
using Agree.Athens.Domain.Aggregates.Account.Factories;
using System.Web;

namespace Agree.Athens.Domain.Services
{
    public class AccountService
    {
        private readonly IHashProvider _hashProvider;
        private readonly IMailService _mailService;
        private readonly IAccountRepository _accountRepository;

        public AccountService(IHashProvider hashProvider,
                              IMailService mailService,
                              IAccountRepository accountRepository)
        {
            _hashProvider = hashProvider;
            _mailService = mailService;
            _accountRepository = accountRepository;
        }

        public async Task<UserAccount> Register(string userName, string email, string password, string confirmationUrl)
        {
            try
            {
                var passwordHash = _hashProvider.Hash(password);
                var account = new UserAccount(userName, email, passwordHash);

                if (await _accountRepository.EmailIsInUse(email))
                {
                    account.AddError(email, $"{account} Email is already in use", email);
                }

                while (await _accountRepository.TagIsInUse(account.Tag, account.UserName))
                {
                    account.UpdateTag(UserTagFactory.CreateRandomUserTag());
                }

                if (account.IsInvalid)
                {
                    throw new ValidationException(account);
                }

                await _accountRepository.AddAsync(account);
                await _accountRepository.UnitOfWork.Commit();

                var confirmationUrlWithToken = AddTokenToMailConfirmationUrl(confirmationUrl, account.Id);

                await _mailService.SendAccountConfirmationMailAsync(account, confirmationUrlWithToken);

                return account;
            }
            catch (Exception ex)
            {
                await _accountRepository.UnitOfWork.Rollback();
                throw ex;
            }
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

        public async Task ConfirmEmail(Guid id)
        {
            try
            {
                var account = await _accountRepository.GetByIdAsync(id);
                if (account is null)
                {
                    throw new EntityNotFoundException(account);
                }

                account.VerifyEmail();

                await _accountRepository.UpdateAsync(account);
                await _accountRepository.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                await _accountRepository.UnitOfWork.Rollback();
                throw ex;
            }
        }
    }
}
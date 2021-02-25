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

                await _mailService.SendAccountConfirmationMailAsync(account, confirmationUrl);

                return account;
            }
            catch (Exception ex)
            {
                await _accountRepository.UnitOfWork.Rollback();
                throw ex;
            }
        }
    }
}
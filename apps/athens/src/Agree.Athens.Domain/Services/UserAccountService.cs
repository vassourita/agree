using System.Collections.Generic;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Domain.Interfaces.Providers;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Domain.Aggregates.Account.Factories;
using System.Web;

namespace Agree.Athens.Domain.Services
{
    public class UserAccountService
    {
        private readonly IHashProvider _hashProvider;
        private readonly IAccountRepository _accountRepository;

        public UserAccountService(IHashProvider hashProvider, IAccountRepository accountRepository)
        {
            _hashProvider = hashProvider;
            _accountRepository = accountRepository;
        }

        public async Task<UserAccount> Register(string userName, string email, string password)
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

                return account;
            }
            catch (Exception ex)
            {
                await _accountRepository.UnitOfWork.Rollback();
                throw ex;
            }
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
using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Domain.Interfaces.Providers;
using Agree.Athens.Domain.Interfaces.Repositories;

namespace Agree.Athens.Domain.Services
{
    public class AccountService : IDomainService
    {
        private readonly IHashProvider _hashProvider;
        private readonly IAccountRepository _accountRepository;

        public AccountService(IHashProvider hashProvider, IAccountRepository accountRepository)
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
    }
}
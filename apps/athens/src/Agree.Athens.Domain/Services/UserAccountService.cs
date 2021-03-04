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

                if (await _accountRepository.EmailIsInUseAsync(email))
                {
                    account.AddError("Email", "Email is already in use by another account", email);
                }

                while (await _accountRepository.TagIsInUseAsync(account.Tag, account.UserName))
                {
                    account.UpdateTag(UserTagFactory.CreateRandomUserTag());
                }

                if (account.IsInvalid)
                {
                    throw new DomainValidationException(account);
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

        public async Task<UserAccount> Login(string email, string password)
        {
            var account = await _accountRepository.GetByEmailAsync(email);
            if (account is null)
            {
                throw DomainUnauthorizedException.InvalidLogin();
            }
            var passwordsMatch = _hashProvider.Compare(password, account.PasswordHash);
            if (!passwordsMatch)
            {
                throw DomainUnauthorizedException.InvalidLogin();
            }
            if (!account.EmailVerified)
            {
                throw DomainUnauthorizedException.AccountNotVerified();
            }
            return account;
        }

        public async Task<UserAccount> Update(Guid userId, string newEmail, string newUserName, UserTag newTag, string passwordConfirm)
        {
            try
            {
                var account = await _accountRepository.GetByIdAsync(userId);
                if (account is null)
                {
                    throw new EntityNotFoundException(account);
                }

                var passwordsMatch = _hashProvider.Compare(passwordConfirm, account.PasswordHash);
                if (!passwordsMatch)
                {
                    throw DomainUnauthorizedException.InvalidPassword();
                }

                if (!string.IsNullOrEmpty(newEmail))
                {
                    if (account.Email == newEmail)
                    {
                        account.AddError("Email", "New account email is the same as the old one");
                    }
                    var userWithSameMail = await _accountRepository.GetByEmailAsync(newEmail);
                    if (userWithSameMail != null && userWithSameMail.Id != account.Id)
                    {
                        account.AddError("Email", "Email is already in use by another account", newEmail);
                    }
                    account.UpdateEmail(newEmail);
                }

                if (!string.IsNullOrEmpty(newUserName))
                {
                    if (account.UserName == newUserName)
                    {
                        account.AddError("UserName", "New account userName is the same as the old one");
                    }
                    account.UpdateUserName(newUserName);
                }

                if (newTag != null && newTag.IsValid)
                {
                    var tagIsInUse = await _accountRepository.TagIsInUseAsync(newTag, account.UserName);
                    if (account.Tag.ToString() == newTag.ToString() && tagIsInUse)
                    {
                        account.AddError("Tag", "New account tag is the same as the old one", newTag);
                    }
                    else if (tagIsInUse)
                    {
                        account.AddError("Tag", "Tag is already in use by another account with same userName", newTag);
                    }
                    account.UpdateTag(newTag);
                }

                if (account.IsInvalid)
                {
                    throw new DomainValidationException(account);
                }

                await _accountRepository.UpdateAsync(account);
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

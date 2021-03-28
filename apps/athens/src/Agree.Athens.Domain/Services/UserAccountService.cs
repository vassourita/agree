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
using Agree.Athens.Domain.Dtos;
using Agree.Athens.Domain.Dtos.Validators;

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

        public async Task<UserAccount> Register(CreateAccountDto createAccountDto)
        {
            createAccountDto.Validate(new CreateAccountDtoValidator());
            if (createAccountDto.IsInvalid)
            {
                throw new DomainValidationException(createAccountDto);
            }

            try
            {
                var passwordHash = _hashProvider.Hash(createAccountDto.Password);
                var account = new UserAccount(createAccountDto.UserName, createAccountDto.Email, passwordHash);

                if (await _accountRepository.EmailIsInUseAsync(createAccountDto.Email))
                {
                    account.AddError("Email", "Email is already in use by another account", createAccountDto.Email);
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

        public async Task<UserAccount> Login(LoginDto loginDto)
        {
            loginDto.Validate(new LoginDtoValidator());
            if (loginDto.IsInvalid)
            {
                throw new DomainValidationException(loginDto);
            }

            var account = await _accountRepository.GetByEmailAsync(loginDto.Email);
            if (account is null)
            {
                throw DomainUnauthorizedException.InvalidLogin();
            }
            var passwordsMatch = _hashProvider.Compare(loginDto.Password, account.PasswordHash);
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

        public async Task<UserAccount> Update(UpdateAccountDto updateAccountDto)
        {
            try
            {
                var account = await _accountRepository.GetByIdAsync(updateAccountDto.UserId);
                if (account is null)
                {
                    throw new EntityNotFoundException(account);
                }

                var passwordsMatch = _hashProvider.Compare(updateAccountDto.PasswordConfirmation, account.PasswordHash);
                if (!passwordsMatch)
                {
                    throw DomainUnauthorizedException.InvalidPassword();
                }

                if (!string.IsNullOrEmpty(updateAccountDto.Email))
                {
                    if (account.Email == updateAccountDto.Email)
                    {
                        account.AddError("Email", "New account email is the same as the old one");
                    }
                    var userWithSameMail = await _accountRepository.GetByEmailAsync(updateAccountDto.Email);
                    if (userWithSameMail != null && userWithSameMail.Id != account.Id)
                    {
                        account.AddError("Email", "Email is already in use by another account", updateAccountDto.Email);
                    }
                    account.UpdateEmail(updateAccountDto.Email);
                }

                if (!string.IsNullOrEmpty(updateAccountDto.UserName))
                {
                    if (account.UserName == updateAccountDto.UserName)
                    {
                        account.AddError("UserName", "New account userName is the same as the old one");
                    }
                    account.UpdateUserName(updateAccountDto.UserName);
                }

                var newTag = new UserTag(updateAccountDto.Tag);
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

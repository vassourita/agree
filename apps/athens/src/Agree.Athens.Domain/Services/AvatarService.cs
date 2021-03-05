using System.Linq;
using System.IO;
using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Domain.Interfaces.Providers;
using Agree.Athens.Domain.Interfaces.Repositories;

namespace Agree.Athens.Domain.Services
{
    public class AvatarService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IFileStorageProvider _fileStorageProvider;

        public AvatarService(IAccountRepository accountRepository, IFileStorageProvider fileStorageProvider)
        {
            _accountRepository = accountRepository;
            _fileStorageProvider = fileStorageProvider;
        }

        public async Task<UserAccount> UpdateAvatar(Guid userId, Stream fileStream, string contentType)
        {
            try
            {
                var account = await _accountRepository.GetByIdAsync(userId);
                if (account is null)
                {
                    throw new EntityNotFoundException(account);
                }

                if (account.AvatarUrl != null)
                {
                    var fileName = account.AvatarUrl.Split('/').Last();
                    await _fileStorageProvider.DeleteBlobAsync(fileName);
                }

                var fileExtension = contentType.ToLower() switch
                {
                    "image/jpg" => "jpg",
                    "image/png" => "png",
                    "image/gif" => "gif",
                    _ => "image/jpg"
                };

                var avatarFileName = $"avatar-{account.Id}-{DateTime.UtcNow.ToString("ddMMyyyyHHmmss")}.{fileExtension}";

                var filePath = await _fileStorageProvider.UploadImageAsync(fileStream, avatarFileName, contentType);

                account.UpdateAvatar(filePath);

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
using System.IO;
using System.Threading.Tasks;

namespace Agree.Athens.Domain.Interfaces.Providers
{
    public interface IFileStorageProvider
    {
        Task<string> UploadImageAsync(Stream fileStream, string blobName, string mimetype);

        Task DeleteBlobAsync(string blobName);
    }
}
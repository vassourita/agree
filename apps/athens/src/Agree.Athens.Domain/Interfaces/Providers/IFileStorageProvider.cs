using System.Threading.Tasks;

namespace Agree.Athens.Domain.Interfaces.Providers
{
    public interface IFileStorageProvider
    {
        Task<string> UploadImageAsync(byte[] content, string fileName);
    }
}
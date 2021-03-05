using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Agree.Athens.Domain.Interfaces.Providers;
using Microsoft.Extensions.Options;
using Agree.Athens.Infrastructure.Configuration;
using Azure.Storage.Blobs.Models;

namespace Agree.Athens.Infrastructure.Providers
{
    public class AzuriteStorageProvider : IFileStorageProvider
    {
        private readonly BlobContainerClient _container;

        public AzuriteStorageProvider(IOptions<AzuriteStorageConfiguration> options)
        {
            _container = new BlobContainerClient(options.Value.ConnectionString, options.Value.AvatarBlobContainerName);
            _container.CreateIfNotExists();
        }

        private async Task<string> UploadFileAsync(Stream fileStream, string blobName, string mimetype)
        {
            var blob = _container.GetBlobClient(blobName);
            var blobHttpHeader = new BlobHttpHeaders();
            blobHttpHeader.ContentType = mimetype;
            await blob.UploadAsync(fileStream, blobHttpHeader);
            return blob.Uri.AbsolutePath;
        }

        public async Task<string> UploadImageAsync(Stream fileStream, string blobName, string mimetype)
        {
            return await UploadFileAsync(fileStream, blobName, mimetype);
        }
    }
}
using System.IO;
using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Agree.Athens.Domain.Interfaces.Providers;
using Microsoft.Extensions.Options;
using Agree.Athens.Infrastructure.Configuration;

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

        private async Task<string> UploadFileAsync(byte[] content, string blobName)
        {
            var blob = _container.GetBlobClient(blobName);
            using (var stream = new MemoryStream(content, writable: false))
            {
                await blob.UploadAsync(stream);
            }
            return blob.Uri.AbsolutePath;
        }

        public async Task<string> UploadImageAsync(byte[] content, string blobName)
        {
            return await UploadFileAsync(content, blobName);
        }
    }
}
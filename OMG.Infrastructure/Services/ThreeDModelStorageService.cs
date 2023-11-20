using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace OMG.Infrastructure.Services
{
    public class ThreeDModelStorageService
    {
        const string ContainerName = "3dmodels";
        private readonly BlobServiceClient _blobServiceClient;

        public ThreeDModelStorageService(string connectionString)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }


        public async Task UploadBlobAsync(string userId, string blobName, Stream content)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blobClient = containerClient.GetBlobClient(userId + "/" + blobName);
            await blobClient.UploadAsync(content, true);
        }

        public async Task DeleteBlobAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<Stream> GetBlobAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            var response = await blobClient.DownloadAsync();
            return response.Value.Content;
        }
    }
}

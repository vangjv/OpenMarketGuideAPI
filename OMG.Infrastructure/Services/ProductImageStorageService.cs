using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace OMG.Infrastructure.Services
{
    public class ProductImageStorageService
    {
        const string ContainerName = "productimages";
        private readonly BlobServiceClient _blobServiceClient;

        public ProductImageStorageService(string connectionString)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task UploadBlobAsync(string vendorId, string productId, string imageId, string imageFileName, Stream content)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blobClient = containerClient.GetBlobClient(GetBlobUrl(vendorId, productId, imageId, imageFileName));
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

        public string GetBlobUrl(string vendorId, string productId, string imageId, string imageFileName)
        {
            return $"{vendorId}/{productId}/{imageId}--{imageFileName}";
        }
    }
}

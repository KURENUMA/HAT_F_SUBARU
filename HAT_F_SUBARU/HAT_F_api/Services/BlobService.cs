using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HAT_F_api.Utils;

namespace HAT_F_api.Services
{
    /// <summary>
    /// HAT-F認証サービス
    /// </summary>
    public class BlobService
    {
        private readonly IConfiguration _configuration;
        private static NLog.ILogger _logger;
        private static BlobServiceClient _blobServiceClient;
        private static BlobContainerClient _blobContainerClient;
        private UpdateInfoSetter _updateInfoSetter;

        public BlobService(IConfiguration configuration, NLog.ILogger logger, UpdateInfoSetter updateInfoSetter)
        {
            _configuration = configuration;
            _logger = logger;
            _updateInfoSetter = updateInfoSetter;

            var connectionString = _configuration.GetSection("AzureBlob").GetValue<String>("ConnectionString");
            var containerName = _configuration.GetSection("AzureBlob").GetValue<String>("ContainerName");
            _blobServiceClient = new BlobServiceClient(connectionString);

            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            if (!_blobContainerClient.Exists())
            {
                _blobContainerClient.Create();
            }
        }

        public async Task<string> UploadFile(Stream srcStream, string id, string name)
        {
            var blobClient = _blobContainerClient.GetBlobClient($"{id}/{name}");
            using (srcStream)
            {
                await blobClient.UploadAsync(srcStream, true);
            }
            return "アップロード成功";
        }

        public async Task<Stream> DownloadFile(string id, string name)
        {
            var blobClient = _blobContainerClient.GetBlobClient($"{id}/{name}");
            var download = await blobClient.OpenReadAsync();
            return download;
        }

        public async Task<string> DeleteFile(string id, string name)
        {
            var blobClient = _blobContainerClient.GetBlobClient($"{id}/{name}");
            await blobClient.DeleteIfExistsAsync();
            _logger.Info($"削除成功: {id}/{name}");
            return "削除成功";
        }

        public async Task<List<BlobItem>> ListFiles(string id)
        {
            var blobs = new List<BlobItem>();
            await foreach (var blobItem in _blobContainerClient.GetBlobsAsync(prefix: id))
            {
                var blobClient = _blobContainerClient.GetBlobClient(blobItem.Name);
                BlobProperties props = await blobClient.GetPropertiesAsync();
                blobItem.Metadata.Add("CreatedOn", props.Metadata.ContainsKey("CreatedOn") ? props.Metadata["CreatedOn"] : "-");
                blobItem.Metadata.Add("LastModified", props.Metadata.ContainsKey("LastModified") ? props.Metadata["LastModified"] : "-");

                blobs.Add(blobItem);
            }
            return blobs;
        }
        public async Task<string> SetMetaData(string id, string name, IDictionary<string, string> metaData)
        {
            var blobClient = _blobContainerClient.GetBlobClient($"{id}/{name}");
            await blobClient.SetMetadataAsync(metaData);
            return "成功";
        }
    }
}
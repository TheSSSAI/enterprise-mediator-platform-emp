using Amazon.S3;
using Amazon.S3.Model;
using EnterpriseMediator.AiWorker.Application.Interfaces;
using EnterpriseMediator.AiWorker.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EnterpriseMediator.AiWorker.Infrastructure.Clients
{
    /// <summary>
    /// Adapter for AWS S3 Storage operations.
    /// Handles retrieving file streams securely.
    /// </summary>
    public class S3StorageAdapter : IFileStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly AwsSettings _settings;
        private readonly ILogger<S3StorageAdapter> _logger;

        public S3StorageAdapter(
            IAmazonS3 s3Client,
            IOptions<AwsSettings> settings,
            ILogger<S3StorageAdapter> logger)
        {
            _s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task<Stream> GetFileStreamAsync(string fileKey, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(fileKey))
                throw new ArgumentNullException(nameof(fileKey));

            try
            {
                _logger.LogInformation("Attempting to download file {FileKey} from bucket {Bucket}", fileKey, _settings.S3BucketName);

                var request = new GetObjectRequest
                {
                    BucketName = _settings.S3BucketName,
                    Key = fileKey
                };

                // NOTE: The caller is responsible for disposing the response stream.
                // We do not use 'using' here because we need to return the open stream.
                var response = await _s3Client.GetObjectAsync(request, token);

                _logger.LogInformation("Successfully opened stream for file {FileKey}. Content Length: {Length}", fileKey, response.ContentLength);

                return response.ResponseStream;
            }
            catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogError(ex, "File {FileKey} not found in bucket {Bucket}.", fileKey, _settings.S3BucketName);
                throw new FileNotFoundException($"SOW document not found in storage: {fileKey}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve file {FileKey} from S3.", fileKey);
                throw new InvalidOperationException($"Storage operation failed for key: {fileKey}", ex);
            }
        }
    }
}
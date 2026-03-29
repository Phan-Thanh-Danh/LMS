using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

namespace backend.Repository.Services
{
    public class CloudflareR2Service : IStorageService
    {
        private readonly AmazonS3Client _s3Client;
        private readonly string _bucketName;

        public CloudflareR2Service(IConfiguration configuration)
        {
            var r2Config = configuration.GetSection("CloudflareR2");
            _bucketName = r2Config["BucketName"]!;
            var accessKey = r2Config["AccessKey"]!;
            var secretKey = r2Config["SecretKey"]!;
            var serviceUrl = r2Config["ServiceUrl"]!;

            var s3Config = new AmazonS3Config { ServiceURL = serviceUrl, ForcePathStyle = true };

            _s3Client = new AmazonS3Client(accessKey, secretKey, s3Config);
        }

        public async Task<string> UploadFileAsync(
            Stream fileStream,
            string fileKey,
            string contentType
        )
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileKey,
                InputStream = fileStream,
                ContentType = contentType,
                DisablePayloadSigning = true,
            };

            await _s3Client.PutObjectAsync(putRequest);
            return fileKey;
        }

        public async Task<Stream> GetFileStreamAsync(string fileKey)
        {
            var request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = fileKey,
            };

            var response = await _s3Client.GetObjectAsync(request);
            return response.ResponseStream;
        }

        public async Task<bool> DeleteFileAsync(string fileKey)
        {
            try
            {
                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = fileKey,
                };
                await _s3Client.DeleteObjectAsync(deleteRequest);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteFolderAsync(string prefix)
        {
            try
            {
                var listRequest = new ListObjectsV2Request
                {
                    BucketName = _bucketName,
                    Prefix = prefix,
                };

                var listResponse = await _s3Client.ListObjectsV2Async(listRequest);

                if (listResponse.S3Objects.Count > 0)
                {
                    var deleteRequest = new DeleteObjectsRequest { BucketName = _bucketName };

                    foreach (var obj in listResponse.S3Objects)
                    {
                        deleteRequest.AddKey(obj.Key);
                    }

                    await _s3Client.DeleteObjectsAsync(deleteRequest);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GeneratePresignedUrl(string fileKey, int expiresInMinutes = 15)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = fileKey,
                Expires = DateTime.UtcNow.AddMinutes(expiresInMinutes),
                Protocol = Protocol.HTTPS,
            };

            return _s3Client.GetPreSignedURL(request);
        }
    }
}

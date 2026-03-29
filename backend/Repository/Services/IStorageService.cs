using System.IO;
using System.Threading.Tasks;

namespace backend.Repository.Services
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileKey, string contentType);
        Task<Stream> GetFileStreamAsync(string fileKey);
        Task<bool> DeleteFileAsync(string fileKey);
        Task<bool> DeleteFolderAsync(string prefix);
        string GeneratePresignedUrl(string fileKey, int expiresInMinutes = 15);
    }
}

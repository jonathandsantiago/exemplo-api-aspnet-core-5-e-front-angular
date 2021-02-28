using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IFileStorageClient
    {
        Task<byte[]> DownloadFile(string bucket, string key);
        Task<bool> DeleteFile(string bucket, string key);
        Task<bool> UploadFile(string bucket, string key, byte[] binary);
        Task<bool> FileExists(string bucket, string key);
    }
}

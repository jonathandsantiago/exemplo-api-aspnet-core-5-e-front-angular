using FavoDeMel.Service.Interfaces;
using Minio;
using System.IO;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Storage
{
    public class FileStorageClient : IFileStorageClient
    {
        private readonly MinioClient _client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public FileStorageClient(MinioClient client)
        {
            _client = client;
        }

        public async Task<bool> DeleteFile(string bucket, string key)
        {
            await CriarBucketSeNaoExistir(bucket);
            await _client.RemoveObjectAsync(bucket, key);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<byte[]> DownloadFile(string bucket, string key)
        {
            await CriarBucketSeNaoExistir(bucket);
            byte[] binario = null;

            await _client.GetObjectAsync(bucket, key, stream =>
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    binario = ms.ToArray();
                }
            });

            return binario;
        }

        public async Task<bool> FileExists(string bucket, string key)
        {
            await CriarBucketSeNaoExistir(bucket);
            await _client.StatObjectAsync(bucket, key);
            return true;
        }

        public async Task<bool> UploadFile(string bucket, string key, byte[] binary)
        {
            await CriarBucketSeNaoExistir(bucket);

            using (MemoryStream memStream = new MemoryStream(binary))
            {
                await _client.PutObjectAsync(bucket, key, memStream, memStream.Length);
            }

            return true;
        }

        private async Task CriarBucketSeNaoExistir(string bucket)
        {
            var bucketExiste = await _client.BucketExistsAsync(bucket);

            if (!bucketExiste)
            {
                await _client.MakeBucketAsync(bucket);
            }
        }
    }
}

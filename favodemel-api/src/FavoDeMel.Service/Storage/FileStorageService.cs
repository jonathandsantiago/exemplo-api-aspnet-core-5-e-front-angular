using FavoDeMel.Domain.Configs;
using FavoDeMel.Domain.Models;
using FavoDeMel.Framework.Helpers;
using FavoDeMel.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Storage
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IFileStorageClient _fileStorageClient;
        private readonly ILogger<FileStorageService> _logger;
        private readonly MinioConfig _minioConfig;

        public FileStorageService(IFileStorageClient fileStorageClient,
            MinioConfig minioConfig,
            ILogger<FileStorageService> logger)
        {
            _fileStorageClient = fileStorageClient;
            _logger = logger;
            _minioConfig = minioConfig;
        }

        public async Task<ArquivoResult<Arquivo>> ObterArquivo(string caminhoDoArquivo)
        {
            try
            {
                var arquivo = await _fileStorageClient.DownloadFile(_minioConfig.Bucket, caminhoDoArquivo);

                var result = new ArquivoResult<Arquivo>()
                {
                    Sucesso = true,
                    Result = new Arquivo
                    {
                        Binario = arquivo,
                        ContentType = MimeTypeMap.GetMimeTypeFromFileName(caminhoDoArquivo),
                        Nome = caminhoDoArquivo
                    }
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter arquivo {bucket} {key}", _minioConfig.Bucket, caminhoDoArquivo);
                return new ArquivoResult<Arquivo>
                {
                    Sucesso = false,
                    Mensagem = ex.Message
                };
            }
        }
    }
}
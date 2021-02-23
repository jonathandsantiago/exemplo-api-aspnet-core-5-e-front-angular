using FavoDeMel.Domain.Configs;
using FavoDeMel.Service.Interfaces;
using Microsoft.Extensions.Logging;

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

        //public async Task<ObterArquivoResultado> ObterArquivo(string caminhoDoArquivo)
        //{
        //    try
        //    {
        //        var arquivo = await _fileStorageClient.DownloadFile(_fileStorageServiceConfiguration.Bucket, caminhoDoArquivo);

        //        var output = new ObterArquivoResultado()
        //        {
        //            Sucesso = true,
        //            Arquivo = new Arquivo
        //            {
        //                Binario = arquivo,
        //                ContentType = MimeTypeMap.GetMimeTypeFromFileName(caminhoDoArquivo),
        //                Nome = caminhoDoArquivo
        //            }
        //        };

        //        return output;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao obter arquivo {bucket} {key}", _fileStorageServiceConfiguration.Bucket, caminhoDoArquivo);
        //        return new ObterArquivoResultado
        //        {
        //            Sucesso = false,
        //            Mensagem = ex.Message
        //        };
        //    }
        //}
    }
}

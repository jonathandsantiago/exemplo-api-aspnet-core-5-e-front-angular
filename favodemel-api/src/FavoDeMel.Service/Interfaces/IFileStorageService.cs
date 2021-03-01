using FavoDeMel.Domain.Models;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IFileStorageService
    {
        Task<ArquivoResult<Arquivo>> ObterArquivo(string caminhoDoArquivo);
    }
}

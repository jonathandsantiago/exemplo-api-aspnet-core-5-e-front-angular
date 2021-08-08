using System.Threading.Tasks;

namespace FavoDeMel.Domain.Interfaces
{
    public interface IServiceCache
    {
        /// <summary>
        /// Método responsável por obter o cache pela chave
        /// </summary>
        /// <param name="key"></param>
        Task<T> ObterAsync<T>(string key);
        /// <summary>
        /// Método responsável por salvar o cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="objeto"></param>
        /// <param name="timeToLive"></param>
        /// <returns></returns>
        Task SalvarAsync<T>(string key, T objeto, int timeToLive = 300);
        /// <summary>
        /// Método responsável por remover o cache pela chave
        /// </summary>
        /// <param name="key"></param>
        Task RemoverAsync(string key);
    }
}
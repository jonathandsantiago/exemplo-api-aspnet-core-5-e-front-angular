using System.Threading.Tasks;

namespace FavoDeMel.Domain.Interfaces
{
    public interface IServiceCache
    {
        /// <summary>
        /// Obtem a entidade em cache pela chave de forma assíncrona
        /// </summary>
        /// <param name="chave">Chave de registro do cache</param>
        /// <returns>Retorna o T em cache</returns>
        Task<T> ObterAsync<T>(string chave);
        /// <summary>
        /// Salva a entidade em cache pela chave de forma assíncrona
        /// </summary>
        /// <param name="chave">Chave de registro do cache</param>
        /// <param name="entidade">Endiade a ser salva no cache</param>
        /// <param name="cicloDeVida">Ciclo de vida do cache</param>
        Task SalvarAsync<T>(string chave, T entidade, int cicloDeVida = 300);
        /// <summary>
        /// Remove o cache pela chave pela chave de forma assíncrona
        /// </summary>
        /// <param name="chave">Chave de registro do cache</param>
        Task RemoverAsync(string chave);
    }
}
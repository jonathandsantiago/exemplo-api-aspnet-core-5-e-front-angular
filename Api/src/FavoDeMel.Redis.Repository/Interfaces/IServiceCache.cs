using System.Threading.Tasks;

namespace FavoDeMel.Redis.Repository.Interfaces
{
    public interface IServiceCache<T>
    {
        Task<T> ObterAsync(string key);

        /// <summary>
        /// Cache object in redis
        /// </summary>
        /// <param name="key">The key to localize cache</param>
        /// <param name="objeto">The Object to cache</param>
        /// <param name="timeToLive">In Seconds</param>
        /// <returns></returns>
        Task SalvarAsync(string key, T objeto, int timeToLive = 300);

        Task RemoverAsync(string key);
    }
}

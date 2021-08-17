using FavoDeMel.Domain.Interfaces;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IMensageriaService
    {
        /// <summary>
        /// Envia a mensagem na fila de forma assíncrona
        /// </summary>
        /// <param name="command">Command de envio</param>
        Task EnviarAsync<T>(T command) where T : IMensageriaCommand;
    }
}

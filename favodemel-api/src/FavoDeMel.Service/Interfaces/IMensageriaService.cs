using FavoDeMel.Domain.Interfaces;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IMensageriaService
    {
        /// <summary>
        /// Método responsável por enviar a mensagem do tipo objeto
        /// </summary>
        /// <param name="command">Command de envio</param>
        /// <returns></returns>
        Task Enviar<T>(T command) where T : IMensageriaCommand;
    }
}

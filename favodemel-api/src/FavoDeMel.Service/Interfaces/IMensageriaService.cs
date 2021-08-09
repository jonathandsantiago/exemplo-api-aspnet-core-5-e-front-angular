using FavoDeMel.Domain.Events;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IMensageriaService
    {
        /// <summary>
        /// Método responsável por enviar a mensagem do tipo objeto
        /// </summary>
        /// <param name="value">Objeto a ser enviado</param>
        /// <param name="topic">Nome da fila</param>
        /// <returns></returns>
        Task<object> Publish<T>(T value, string topic = TopicEvento.FilaPedido);
        /// <summary>
        /// Método responsável por enviar a mensagem
        /// </summary>
        /// <param name="value">Mensagem a ser enviada</param>
        /// <param name="topic">Nome da fila</param>
        /// <returns></returns>
        Task<object> Publish(string value, string topic = TopicEvento.FilaPedido);
    }
}

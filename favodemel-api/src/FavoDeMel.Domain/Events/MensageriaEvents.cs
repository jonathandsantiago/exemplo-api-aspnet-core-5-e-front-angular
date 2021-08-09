using System.Threading.Tasks;

namespace FavoDeMel.Domain.Events
{
    public delegate Task MensagemEvent(string mensagem);

    public class MensageriaEvents
    {
        public event MensagemEvent Mensagem;

        public void EnviarMensagem(string mensagem)
        {
            Mensagem?.Invoke(mensagem);
        }
    }
}

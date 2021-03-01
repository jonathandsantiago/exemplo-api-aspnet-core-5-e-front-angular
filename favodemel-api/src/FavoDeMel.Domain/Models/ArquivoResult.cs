namespace FavoDeMel.Domain.Models
{
    public class ArquivoResult<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public T Result { get; set; }
    }
}

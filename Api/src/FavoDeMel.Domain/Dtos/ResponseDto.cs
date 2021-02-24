namespace FavoDeMel.Domain.Dtos
{
    public class ResponseDto<T> : IResponseDto
    {
        public string Mensagem { get; set; }
        public T Resultado { get; set; }
        public ResponseTipo Tipo { get; set; }
    }

    public interface IResponseDto
    {
        string Mensagem { get; set; }
        ResponseTipo Tipo { get; set; }
    }

    public enum ResponseTipo
    {
        Default,
        Excel,
        Zip,
        Txt,
    }
}

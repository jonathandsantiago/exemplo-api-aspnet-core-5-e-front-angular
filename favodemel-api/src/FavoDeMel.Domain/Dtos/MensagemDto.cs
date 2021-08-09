namespace FavoDeMel.Domain.Dtos
{
    public class MensagemDto
    {
        public string Evento { get; set; }
        public string Value { get; set; }

        public MensagemDto(string evento, string value)
        {
            Evento = evento;
            Value = value;
        }
    }
}

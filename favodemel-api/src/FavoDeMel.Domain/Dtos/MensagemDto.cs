namespace FavoDeMel.Domain.Dtos
{
    public class MensagemDto
    {
        public string Topic { get; set; }
        public string Value { get; set; }

        public MensagemDto(string topic, string value)
        {
            Topic = topic;
            Value = value;
        }
    }
}

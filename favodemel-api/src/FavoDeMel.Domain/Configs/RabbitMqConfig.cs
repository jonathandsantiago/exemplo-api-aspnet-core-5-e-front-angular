using Microsoft.Extensions.Configuration;

namespace FavoDeMel.Domain.Configs
{
    public class RabbitMqConfig
    {
        public string Url { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string VirtualHost { get; set; }

        public RabbitMqConfig(IConfiguration configuration)
        {
            Url = configuration["RabbitMq:Url"];
            Usuario = configuration["RabbitMq:Usuario"];
            Senha = configuration["RabbitMq:Senha"];
            VirtualHost = configuration["RabbitMq:Vhost"];
        }
    }
}

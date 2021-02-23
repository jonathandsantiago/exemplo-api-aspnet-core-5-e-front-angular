using Microsoft.Extensions.Configuration;

namespace FavoDeMel.Domain.Configs
{
    public class RedisConfig
    {
        public string Connection { get; set; }
        public string Instance { get; set; }

        public RedisConfig(IConfiguration configuration)
        {
            Connection = configuration["RabbitMq:Connection"];
            Instance = configuration["RabbitMq:Instance"];
        }
    }
}

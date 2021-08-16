using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FavoDeMel.Domain.Models.Settings
{
    public class RabbitMqSettings : ISettings
    {
        public string Url { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Vhost { get; set; }

        public RabbitMqSettings(IConfiguration configuration)
        {
            new ConfigureFromConfigurationOptions<RabbitMqSettings>(configuration.GetSection("RabbitMq")).Configure(this);
        }

        public override string ToString()
        {
            return StringHelper.ConcatLogConfig(this);
        }
    }
}
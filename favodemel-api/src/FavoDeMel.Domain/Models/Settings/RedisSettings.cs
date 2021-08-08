using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FavoDeMel.Domain.Models.Settings
{
    public class RedisSettings : ISettings
    {
        public string Connection { get; set; }
        public string Instance { get; set; }

        public RedisSettings(IConfiguration configuration)
        {
            new ConfigureFromConfigurationOptions<RedisSettings>(configuration.GetSection("Redis")).Configure(this);
        }

        public override string ToString()
        {
            return StringHelper.ConcatLogConfig(this);
        }
    }
}
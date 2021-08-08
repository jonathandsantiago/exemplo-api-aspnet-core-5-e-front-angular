using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace FavoDeMel.Domain.Models.Settings
{
    public class KafkaSettings : ISettings
    {
        public string BootstrapServers { get; set; }
        public IEnumerable<string> Topics { get; set; }

        public KafkaSettings(IConfiguration configuration)
        {
            new ConfigureFromConfigurationOptions<KafkaSettings>(configuration.GetSection("kafka")).Configure(this);
        }

        public override string ToString()
        {
            return StringHelper.ConcatLogConfig(this);
        }
    }
}
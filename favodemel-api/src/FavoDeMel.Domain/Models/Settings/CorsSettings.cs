using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FavoDeMel.Domain.Models.Settings
{
    public class CorsSettings : ISettings
    {
        public string Policy { get; set; }
        public string[] WithOrigins { get; set; }

        public CorsSettings(IConfiguration configuration)
        {
            new ConfigureFromConfigurationOptions<CorsSettings>(configuration.GetSection("Cors")).Configure(this);
        }

        public override string ToString()
        {
            return StringHelper.ConcatLogConfig(this);
        }
    }
}

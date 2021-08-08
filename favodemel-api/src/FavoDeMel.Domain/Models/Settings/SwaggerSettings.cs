using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FavoDeMel.Domain.Models.Settings
{
    public class SwaggerSettings : ISettings
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string ContactName { get; set; }
        public string ContactUrl { get; set; }
        public string SecurityDescription { get; set; }
        public string SecurityName { get; set; }
        public string UrlEndpoint { get; set; }
        public string NameEndpoint { get; set; }

        public SwaggerSettings(IConfiguration configuration)
        {
            new ConfigureFromConfigurationOptions<SwaggerSettings>(configuration.GetSection("Swagger")).Configure(this);
        }

        public override string ToString()
        {
            return StringHelper.ConcatLogConfig(this);
        }
    }
}
using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FavoDeMel.Domain.Models.Settings
{
    public class AuthSettings : ISettings
    {
        public bool IsEnabled { get; set; }
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Seconds { get; set; }

        public AuthSettings(IConfiguration configuration)
        {
            new ConfigureFromConfigurationOptions<AuthSettings>(configuration.GetSection("Authentication:JwtBearer")).Configure(this);
        }

        public override string ToString()
        {
            return StringHelper.ConcatLogConfig(this);
        }
    }
}
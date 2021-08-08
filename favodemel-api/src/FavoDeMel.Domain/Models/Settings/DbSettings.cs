using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FavoDeMel.Domain.Models.Settings
{
    public class DbSettings : ISettings
    {
        public string ConnectionName { get; set; }

        public DbSettings(IConfiguration configuration)
        {
            new ConfigureFromConfigurationOptions<DbSettings>(configuration.GetSection("ConnectionStrings")).Configure(this);
        }

        public override string ToString()
        {
            return StringHelper.ConcatLogConfig(this);
        }
    }
}
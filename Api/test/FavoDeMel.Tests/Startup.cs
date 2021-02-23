using Microsoft.Extensions.Configuration;

namespace FavoDeMel.Tests
{
    public class Startup
    {
        private static IConfigurationRoot _configuration;

        public static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new Startup().GetIConfigurationRoot();
                }

                return _configuration;
            }
        }

        public IConfigurationRoot GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}

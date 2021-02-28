using FavoDeMel.Domain.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;

namespace FavoDeMel.Domain.Configs
{
    public class MinioConfig
    {
        public string Endpoint { get; set; }
        public bool ForceSsl { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Bucket { get; set; }

        public MinioConfig(IConfiguration configuration)
        {
            Endpoint = configuration["MinioSettings:Endpoint"];
            ForceSsl = Convert.ToBoolean(configuration["MinioSettings:ForceSsl"]);
            AccessKey = configuration["MinioSettings:AccessKey"];
            SecretKey = configuration["MinioSettings:SecretKey"];
            Bucket = configuration["MinioSettings:Bucket"];
        }
    }
}

using FavoDeMel.IoC.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;

namespace FavoDeMel.IoC
{
    public static  class InjectorAuth
    {
        public static IServiceCollection AddJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureToken(services, configuration, out SigningConfiguration signingConfiguration, out TokenConfiguration tokenConfiguration);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                TokenValidationParameters paramsValidation = options.TokenValidationParameters;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.IssuerSigningKey = signingConfiguration.Key;

                paramsValidation.ValidateAudience = true;
                paramsValidation.ValidAudience = tokenConfiguration.Audience;

                paramsValidation.ValidateIssuer = true;
                paramsValidation.ValidIssuer = tokenConfiguration.Issuer;

                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.FromSeconds(tokenConfiguration.Seconds);              
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            return services;
        }

        private static void ConfigureToken(IServiceCollection services, IConfiguration configuration, out SigningConfiguration signingConfiguration, out TokenConfiguration tokenConfiguration)
        {
            signingConfiguration = new SigningConfiguration();
            services.AddSingleton(signingConfiguration);

            tokenConfiguration = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(configuration.GetSection("Authentication:JwtBearer"))
                .Configure(tokenConfiguration);
            services.AddSingleton(tokenConfiguration);
        }
    }
}
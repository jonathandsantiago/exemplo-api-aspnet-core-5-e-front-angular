using FavoDeMel.Api.Providers.Interface;
using FavoDeMel.Domain.Extensions;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Models.Auths;
using FavoDeMel.Domain.Models.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace FavoDeMel.Api.Providers
{
    public class AuthProvider : IApiProvider
    {
        public void AddProvider(IServiceCollection services, ISettings<string, object> settings)
        {
            SigningConfiguration signingConfiguration = new SigningConfiguration();
            AuthSettings authSettings = settings.GetSetting<AuthSettings>();
            services.AddSingleton(signingConfiguration);
            services.AddSingleton(authSettings);

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
                paramsValidation.ValidAudience = authSettings.Audience;

                paramsValidation.ValidateIssuer = true;
                paramsValidation.ValidIssuer = authSettings.Issuer;

                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.FromSeconds(authSettings.Seconds);
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(JwtBearerDefaults.AuthenticationScheme, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}
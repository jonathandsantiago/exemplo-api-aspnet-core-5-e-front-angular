using FavoDeMel.Api.Providers.Interface;
using FavoDeMel.Domain.Extensions;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Models.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace FavoDeMel.Api.Providers
{
    public class SwaggerProvider : IApiProvider
    {
        public void AddProvider(IServiceCollection services, ISettings<string, object> settings)
        {
            SwaggerSettings authSettings = settings.GetSetting<SwaggerSettings>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(authSettings.Version,
                    new OpenApiInfo
                    {
                        Title = authSettings.Title,
                        Version = authSettings.Version,
                        Description = authSettings.Description,
                        Contact = new OpenApiContact
                        {
                            Name = authSettings.ContactName,
                            Url = new Uri(authSettings.ContactUrl)
                        }
                    });

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = authSettings.SecurityDescription,
                    Name = authSettings.SecurityName,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id =JwtBearerDefaults.AuthenticationScheme }
                            }, new List<string>() }
                    });
            });
        }
    }
}
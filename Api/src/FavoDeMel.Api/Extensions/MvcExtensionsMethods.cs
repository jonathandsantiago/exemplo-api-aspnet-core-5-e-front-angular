using FavoDeMel.Domain.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace FavoDeMel.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class MvcExtensionsMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                      new OpenApiInfo
                      {
                          Title = "Api Restaurante Favo de Mel",
                          Version = "v1",
                          Description = "API REST Restaurante Favo de Mel",
                          Contact = new OpenApiContact
                          {
                              Name = "Jonathan D. C. Santiago",
                              Url = new Uri("https://github.com/jonathandsantiago/Arquitetura")
                          }
                      });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer" }
                            }, new List<string>() }
                    });
            });

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static IApplicationBuilder UseSwaggerGen(this IApplicationBuilder app)
        {
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Restaurante Favo de Mel");
                });
            return app;
        }
    }
}

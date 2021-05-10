using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Gozen.Service.PassengerApi.Helpers
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("SwaggerDoc", new OpenApiInfo
                {
                    Title = "Galaksity Communication API",
                    Version = "1.0.0",
                    Description = "Terminal Communications",
                    Contact = new OpenApiContact
                    {
                        Name = "Gozen.Service.PassengerDto.Api",
                        Url = new Uri("http://www.galaksity.com"),
                        Email = "info@galaksity.com"
                    },
                    TermsOfService = new Uri("http://swagger.io/terms/")
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/SwaggerDoc/swagger.json", "Galaksity Communication API");
                });
            return app;
        }
    }
}
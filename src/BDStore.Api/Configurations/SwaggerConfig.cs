using Microsoft.OpenApi.Models;

namespace BDStore.Api.Configurations
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BDStore",
                    Description =
                        "Essa api de um e-commercee faz parte de meus estudos para me tornar um programador back-end",
                    Contact = new OpenApiContact
                    {
                        Name = "Bruno Dias",
                        Email = "brunohdias95@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o tokenk JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] { }
                    }
                });
            });
        }

        public static WebApplication UseSwaggerConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(config => { config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
            }

            return app;
        }
    }
}
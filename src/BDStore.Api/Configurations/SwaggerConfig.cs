
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
                    Description = "Essa api de um e-commercee faz parte de meus estudos para me tornar um programador back-end",
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

            });

        }

        public static WebApplication UseSwaggerConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }

            return app;
        }
    }
}
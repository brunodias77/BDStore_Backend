using System.Text;
using BDStore.Domain.ValueObjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BDStore.Api.Configurations
{
    public static class JwtConfig
    {
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            services.Configure<AppSettings>(jwtSettings);
            var appSettings = jwtSettings.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            // if (key.Length < 64)
            // {
            //     throw new Exception(
            //         "A chave JWT deve ter pelo menos 64 bytes de comprimento para ser compatível com HMAC-SHA512.");
            // }
            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new
                        SymmetricSecurityKey(key)
                };
            });
        }
    }
}
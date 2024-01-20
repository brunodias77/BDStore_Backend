using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDStore.Application.Common.Interfaces;
using BDStore.Infra.Data;
using BDStore.Infra.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BDStore.Infra.Configurations
{
    public static class InfraConfig
    {
        public static IServiceCollection AddInfraConfig(this IServiceCollection services, IConfiguration configuration)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            return services;
        }
    }
}
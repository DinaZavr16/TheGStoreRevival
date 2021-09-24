using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGStore.BLL.Contracts;
using TheGStore.BLL.Managers;

namespace TheGStore.BLL.Extensions
{
    public static class StartupExtension
    {
        public static void AddBllManagers(this IServiceCollection services)
        {
            services.AddScoped<ICountryManager, CountryManager>();
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<IDeveloperManager, DeveloperManager>();
            services.AddScoped<IGameManager, GameManager>();
            services.AddScoped<IOrderManager, OrderManager>();
        }

        public static void ConnectSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0", new OpenApiInfo { Title = "TheGStore", Version = "v1.0" });
            });
        }
    }
}

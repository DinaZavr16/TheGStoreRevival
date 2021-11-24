using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheGStore.BLL.Contracts;
using TheGStore.BLL.Managers;
using TheGStore.DAL;
using Microsoft.OpenApi.Models;

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

        public static void AddAuth(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<TheGStoreDbContext>();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "912919150795-lt5a4gpvos5rq238aj0r8mgnmpa8m55c.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VehicleVision.BLL.Services;
using VehicleVision.DAL.UnitOfWork;

namespace VehicleVision.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IVehiclesInformationService, VehiclesInformationService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug();
            loggerFactory.AddConsole();
            loggerFactory.AddConsole(includeScopes: true);
            loggerFactory.AddDebug(minLevel: LogLevel.Error);
            loggerFactory.AddConsole(minLevel: LogLevel.Warning);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            var supportedCultures = new[]
            {
                    new CultureInfo("en-US"),
            };

            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            app.UseRequestLocalization(requestLocalizationOptions);

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseMvcWithDefaultRoute();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

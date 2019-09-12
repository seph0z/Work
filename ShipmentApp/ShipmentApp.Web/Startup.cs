using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShipmentApp.Data.EntityFramework;
using ShipmentApp.Domain.Contracts;
using ShipmentApp.Domain.Services;
using ShipmentApp.Domain.Services.Exceptions;
using ShipmentApp.Domain.Services.MappingProfiles;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNet.OData.Extensions;
using ShipmentApp.Domain.Services.Observers;
using ShipmentApp.Domain.Contracts.ViewModels;

namespace ShipmentApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            MapsterConfig.ConfigCarrier();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AppDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.EnableAnnotations();
            });

            services.AddScoped<AppDbContext>();
            services.AddScoped<IShipmentService, ShipmentService>();
            services.AddScoped<ICarrierService, CarrierService>();
            services.AddScoped<IActivityLogService, ActivityLogService>();
            services.AddScoped<Reporter>();
            services.AddScoped(sp =>
            {
                var tracker = new Tracker<ShipmentViewModel>();
                sp.GetService<Reporter>().Subscribe(tracker);
                return tracker;
            });
            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //var sh = app.ApplicationServices.GetRequiredService(typeof(ShipmentTracker));.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMiddleware<StatusCodeExceptionHandler>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Shipment}/{action=Index}/{id?}");
                routes.EnableDependencyInjection();
                routes.Expand().Select().Count().OrderBy().Filter();
            });
        }
    }
}

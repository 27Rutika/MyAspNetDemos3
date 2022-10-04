using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyAngularDemo.Data;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace MyAngularDemo
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
            // NOTE: This should ALWAYS BE the FIRST service registered in the ConfigureServices() method
            // Register EntityFramework Core Services to use SQL Server
            // Register ApplicationDbContext as a Service that can be used using Dependency Injection (DI) in any Controller
            services
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    // Get the SQL Connection String from the AppSettings.json file
                    string connString = Configuration.GetConnectionString("MyDefaultConnectionString");

                    options.UseSqlServer(connString);
                });

            //Register the MVC Middleware
            // Needed for swagger Documentation Middleware Services
            // Needed for API Support(if applicable)
            services.AddMvc();

            //Register the swagger deocumentation
            services
                .AddSwaggerGen(config =>
                {
                    config.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "My Angular Demo",
                        Description = "My Angular Demo App with OData compliant ASP.NET Web API - API Version 1.0"
                    });
                });

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Add the Swagger Middleware
            app.UseSwagger();

            // Add the Swagger Documentation Generation Middleware
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "MyLMS Web API v1.0");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}

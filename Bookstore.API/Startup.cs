using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.API.Middleware;
using Bookstore.Application.Entities;
using Bookstore.Infrastructure.Configuration;
using Bookstore.Infrastructure.Database;
using Bookstore.Infrastructure.Utilities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Bookstore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(op => op.Filters.Add(typeof(HttpGlobalExceptionFilter)))
                .AddFluentValidation();
            services.AddTransient<IValidator<Category>, categoryValidator>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptionsMonitor<AppSettings>>().CurrentValue);
            
            services.AddTransient(service  => new CategoryMongoDb(
                Helper.Settings.ConnectionStringSettings.mongodbConnectString, 
                Helper.Settings.ConnectionStringSettings.MongoDatabaseName, 
                Helper.Settings.ConnectionStringSettings.CategoryCollectionName));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            AppSettingServices.Services = app.ApplicationServices;
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

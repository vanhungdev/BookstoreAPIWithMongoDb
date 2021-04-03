using Bookstore.API.Infrastructure.Filters;
using Bookstore.API.Infrastructure.Middleware;
using Bookstore.Application.Book;
using Bookstore.Application.Category;
using Bookstore.Infrastructure.Configuration;
using Bookstore.Infrastructure.Entities;
using Bookstore.Infrastructure.Utilities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(ValidateModelStateFilter));
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddTransient<IValidator<Category>, categoryValidator>();
            services.AddTransient<IValidator<Book>, BookValidator>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptionsMonitor<AppSettings>>().CurrentValue);
            
            services.AddTransient(service  => new CategoryMongoDb(
                Helper.Settings.ConnectionStringSettings.mongodbConnectString, 
                Helper.Settings.ConnectionStringSettings.MongoDatabaseName, 
                Helper.Settings.ConnectionStringSettings.CategoryCollectionName));
            services.AddTransient(service => new BookMongoDb(
                Helper.Settings.ConnectionStringSettings.mongodbConnectString,
                Helper.Settings.ConnectionStringSettings.MongoDatabaseName,
                Helper.Settings.ConnectionStringSettings.BookCollectionName));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            AppSettingServices.Services = app.ApplicationServices;
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

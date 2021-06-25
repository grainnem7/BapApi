using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BapApi.Models;

namespace BapApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// The ServiceCollection Dependency Injection (DI) is supported and applied for BAP services. 
        /// This is straight as we just add it to the IServiceCollection in the ConfigureServices() 
        /// pipeline.
        /// https://tim-maes.com/2019/10/21/net-core-tutorial-using-the-servicecollection-extension-pattern/
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite().AddDbContext<StoreAppsContext>();
            services.AddControllers();
        }

        /// <summary>
        /// IApplicationBuilder Provides help configure and start web applications pages.
        /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting?view=aspnetcore-5.0
        /// https://andrewlock.net/comparing-startup-between-the-asp-net-core-3-templates/
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
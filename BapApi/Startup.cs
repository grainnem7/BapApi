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
        /// [1] The ServiceCollection Dependency Injection (DI) is supported and applied for BAP services. 
        /// This is straight as we just add it to the IServiceCollection in the ConfigureServices() 
        /// pipeline. For rmore inforrmation see the link below.
        /// https://tim-maes.com/2019/10/21/net-core-tutorial-using-the-servicecollection-extension-pattern/
        /// 
        /// [2] REST Web API is a light-weight essential component of web development in order to share the
        /// data across multiple client machines or devices e.g. mobile devices, desktop applications or any 
        /// website. Authorization of REST Web API is an equally important part for sharing data across 
        /// multiple client machines and devices in order to protect data sensitivity from any outside breaches 
        /// and to authenticate the utilization of the target REST Web API. 
        /// 
        /// Authorization of REST Web API can be done via a specific username/password with the combination of 
        /// a secret key, but, for this type of authorization scheme, REST Web API access needs to be authenticated 
        /// per call to the hosting server.Also, the owner of the server have no way to verify who is utilizing our
        /// API, whether it's the clients that we have allowed access to or some malicious user using our API(s)
        /// without our knowledge. And since the username/password is packed as a base64 format automatically by 
        /// the browser, any malicious user tracing my browsers activity can get hold of my API calls and  easily 
        /// decrypt base64 format and could use API for malicious activities. 
        /// 
        /// So the new authorization scheme is introduced which can also be utilized in Login flow of any web application as well, 
        /// this  authorization is the OAuth 2.0 which is a token based authorization scheme. This is the type of authorization
        /// intended to secure this API.  For more information please visit the following links below
        /// https://www.c-sharpcorner.com/article/asp-net-mvc-oauth-2-0-rest-web-api-authorization-using-database-first-approach/
        /// https://procodeguide.com/programming/oauth2-and-openid-connect-in-aspnet-core/
        /// https://en.wikipedia.org/wiki/OAuth
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
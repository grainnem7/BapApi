using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BapApi.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BapApi
{
    //testing commits
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
            /// [1] API Documentations softwaares are great but the This is a common problem with
            /// auto-generated docs or docs that are neglected by creators. Although many 
            /// documentation generation tools are doing a great job at commenting on the code, 
            /// they cannot replace actual explanations in English written by a developer or technical
            /// writer.
            /// 
            /// Thus creating good docs is almost as important as building a good API. Because poorly
            /// written docs or the ones that can’t be found by simply searching for “ X Product API” 
            /// are failing the whole development effort. To creaate good API doc read the link below
            /// https://www.altexsoft.com/blog/api-documentation/
            /// 
            /// [2] ASP.NET Core 2.x apps is the conspicuous use of endpoint routing. This was introduced in
            /// 2.2, but could only be used for MVC controllers. In 3.0, endpoint routing is the preferred
            /// approach, with the most basic setup provided in the ms docs template. Thus endpoint routing 
            /// separates the process of selecting which "endpoint" will execute from the actual running of 
            /// the endpoint such as an endpoint consists of a path pattern, and something to execute when
            /// called.It could be an MVC action on a controller or it could be a simple lambda function.
            /// 
            /// The UseRouting() extension method is what looks at the incoming request and decides which 
            /// endpoint should execute. Any middleware that appears after the UseRouting() call will know
            /// which endpoint will run eventually.The UseEndpoints() call is responsible for configuring 
            /// the endpoints, but also for executing them. To learn more please visit the link below.
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
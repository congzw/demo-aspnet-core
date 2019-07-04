using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MyWebApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.Run(async (context) =>
            {
                if (context.Request.Path == "/")
                {
                    await Task.Run(() => context.Response.Redirect("index.html", false));
                }
                await context.Response.WriteAsync("Hello World! Path: " + context.Request.Path);
            });
        }
    }
}

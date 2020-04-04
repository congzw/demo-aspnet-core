using DemoDI.Demos;
using DemoDI.Demos.InitTasks;
using DemoDI.Demos.ObjectTraces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace DemoDI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDemos();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            LoopTaskHelper.StartLoop("Foo", () => LoopTask(app.ApplicationServices), TimeSpan.FromMilliseconds(200));
        }

        private static void LoopTask(IServiceProvider sp)
        {
            //var dbContext = sp.GetService<TraceDbContext>();
            using (IServiceScope scope = sp.CreateScope())
            {
                var scopeSp = scope.ServiceProvider;
                var dbContext = scopeSp.GetService<TraceDbContext>();
            }
        }
    }
}

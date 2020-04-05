using DemoDI.Demos;
using DemoDI.Demos.InitTasks;
using DemoDI.Demos.ObjectTraces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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

            services.AddServiceLocatorHttpAdapter();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseServiceLocatorHttpAdapter();

            //LoopTaskHelper.StartLoop("Foo", () => LoopTask(app.ApplicationServices), TimeSpan.FromMilliseconds(5000));
            LoopTaskHelper.StartLoop("Foo", () => LoopTask2(), TimeSpan.FromMilliseconds(5000));
        }

        private static void LoopTask(IServiceProvider sp)
        {
            using (IServiceScope scope = sp.CreateScope())
            {
                var scopeSp = scope.ServiceProvider;
                var dbContext = scopeSp.GetRequiredService<TraceDbContext>();
            }
        }

        private static void LoopTask2()
        {
            IServiceProvider sp = ServiceLocator.Provider;

            using (IServiceScope scope = sp.CreateScope())
            {
                var test = scope.ServiceProvider.GetService<TraceDbContext>();
            }
        }
    }
}

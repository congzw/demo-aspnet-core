using DemoDI.Demos;
using DemoDI.Demos.InitTasks;
using DemoDI.Demos.ObjectTraces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

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

            services.AddServiceProviderLocatorHttp();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            
            app.UseServiceProviderLocatorHttp();

            //LoopTaskHelper.StartLoop("Foo", () => LoopTask(app.ApplicationServices), TimeSpan.FromMilliseconds(5000));
            //LoopTaskHelper.StartLoop("Foo", () => LoopTask2(), TimeSpan.FromMilliseconds(5000));
            //TestServiceProvider(app);
        }

        private static void TestServiceProvider(IApplicationBuilder app)
        {
            //Microsoft.Extensions.DependencyInjection.ServiceProvider: 38524289
            //Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceProviderEngineScope: 11174282
            //Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceProviderEngineScope: 33459681
            //Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceProviderEngineScope: 33459681

            TraceIt(app.ApplicationServices);
            using (var scope = app.ApplicationServices.CreateScope())
            {
                TraceIt(scope.ServiceProvider);
            }

            TraceIt(app.ApplicationServices.GetRequiredService<IServiceProvider>());
            TraceIt(app.ApplicationServices.GetRequiredService<IServiceProvider>());
        }

        private static void TraceIt(object instance)
        {
            Trace.WriteLine(string.Format("{0}: {1}", instance.GetType().FullName, instance.GetHashCode()));
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
            //using (var scope = ServiceProviderLocator.Provider.CreateScope())
            //{
            //    var test = scope.ServiceProvider.GetService<TraceDbContext>();
            //}

            ServiceProviderLocator.Provider.RunInScope(x =>
            {
                var test = x.GetService<TraceDbContext>();
            });
        }
    }
}

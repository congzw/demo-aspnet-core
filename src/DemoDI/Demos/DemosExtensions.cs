using Microsoft.Extensions.DependencyInjection;

namespace DemoDI.Demos
{
    public static class DemosExtensions
    {
        public static void AddDemos(this IServiceCollection services)
        {
            //when use a di,let di do the dispose work!
            services.AddSingleton<MyConfig>();
            services.AddTransient<FooService>();
            services.AddTransient<BarService>();
            services.AddScoped<DbContext>();
            //services.AddTransient<DbContext>();
        }
    }
}

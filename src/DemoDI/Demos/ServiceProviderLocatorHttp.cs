using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DemoDI.Demos
{
    //how to use:
    //1 setup => services.AddServiceProviderLocatorHttp();
    //2 use => app.UseServiceProviderLocatorHttp();
    public class ServiceProviderLocatorHttp : IServiceProviderLocator
    {
        private readonly IServiceProvider _rootServiceProvider;

        public ServiceProviderLocatorHttp(IServiceProvider rootServiceProvider)
        {
            _rootServiceProvider = rootServiceProvider;
        }

        public IServiceProvider GetProvider()
        {
            var httpContext = GetHttpContext();
            if (httpContext == null)
            {
                return _rootServiceProvider;
            }
            return httpContext.RequestServices;
        }

        private HttpContext GetHttpContext()
        {
            var contextAccessor = _rootServiceProvider.GetService<IHttpContextAccessor>();
            if (contextAccessor == null)
            {
                throw new InvalidOperationException("没有初始化: IHttpContextAccessor");
            }
            return contextAccessor.HttpContext;
        }
    }

    public static class ServiceProviderLocatorHttpExtensions
    {
        public static IServiceCollection AddServiceProviderLocatorHttp(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IServiceProviderLocator, ServiceProviderLocatorHttp>();
            return services;
        }

        public static IApplicationBuilder UseServiceProviderLocatorHttp(this IApplicationBuilder app)
        {
            ServiceProviderLocator.Resolve = () => app.ApplicationServices.GetRequiredService<IServiceProviderLocator>().GetProvider();
            return app;
        }

        public static void RunInScope(this IServiceProvider sp, Action<IServiceProvider> action)
        {
            using (var scope = sp.CreateScope())
            {
                action(scope.ServiceProvider);
            }
        }
    }
}

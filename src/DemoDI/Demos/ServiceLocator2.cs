using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace DemoDI.Demos
{
    public interface IServiceLocator
    {
        IServiceProvider GetProvider();
    }

    public sealed class ServiceLocator : IServiceLocator
    {
        public IServiceProvider GetProvider()
        {
            return Resolve == null ? NullServiceProvider.Instance : Resolve();
        }

        public static IServiceLocator Instance = new ServiceLocator();

        #region for easy use and for extensions

        //for easy use
        public static IServiceProvider Provider => Instance.GetProvider();

        // for extensions
        public static Func<IServiceProvider> Resolve = null;

        class NullServiceProvider : IServiceProvider
        {
            public object GetService(Type serviceType)
            {
                return null;
            }
            public static IServiceProvider Instance = new NullServiceProvider();
        }

        #endregion
    }

    //how to use:
    //1 setup => services.AddServiceLocatorHttpAdapter();
    //2 use => app.UseServiceLocatorHttpAdapter();
    public class ServiceLocatorHttpAdapter : IServiceLocator
    {
        private readonly IServiceProvider _rootServiceProvider;

        public ServiceLocatorHttpAdapter(IServiceProvider rootServiceProvider)
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

    public static class ServiceLocatorHttpAdapterExtensions
    {
        public static IServiceCollection AddServiceLocatorHttpAdapter(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IServiceLocator, ServiceLocatorHttpAdapter>();
            return services;
        }

        public static IApplicationBuilder UseServiceLocatorHttpAdapter(this IApplicationBuilder app)
        {
            ServiceLocator.Resolve = () => app.ApplicationServices.GetRequiredService<IServiceLocator>().GetProvider();
            return app;
        }

        public static void RunActionInNewScope(this IServiceProvider sp, Action<IServiceScope> action)
        {
            using (var scope = sp.CreateScope())
            {
                action(scope);
            }
        }
    }
}

//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Mvc.Infrastructure;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection.Extensions;

//namespace Common
//{
//    public interface IServiceLocator
//    {
//        object GetService(Type type);
//        T GetService<T>();
//        IEnumerable<object> GetServices(Type type);
//        IEnumerable<T> GetServices<T>();
//    }

//    public sealed class ServiceLocator
//    {
//        public static IServiceLocator Current => Resolve();

//        // for extensions
//        public static Func<IServiceLocator> Resolve = () => NullServiceLocator.Instance;
//    }

//    public class NullServiceLocator : IServiceLocator
//    {
//        public T GetService<T>()
//        {
//            return default(T);
//        }

//        public IEnumerable<T> GetServices<T>()
//        {
//            return Enumerable.Empty<T>();
//        }

//        public object GetService(Type type)
//        {
//            return null;
//        }

//        public IEnumerable<object> GetServices(Type type)
//        {
//            return Enumerable.Empty<object>();
//        }

//        public static IServiceLocator Instance = new NullServiceLocator();
//    }

//    public class ServiceLocatorHttpAdapter : IServiceLocator
//    {
//        private readonly IServiceProvider _rootServiceProvider;

//        public ServiceLocatorHttpAdapter(IServiceProvider rootServiceProvider)
//        {
//            _rootServiceProvider = rootServiceProvider;
//        }

//        public IServiceProvider GetProvider()
//        {
//            var httpContext = GetHttpContext();
//            if (httpContext == null)
//            {
//                return _rootServiceProvider;
//            }
//            return httpContext.RequestServices;
//        }

//        public object GetService(Type type)
//        {
//            throw new NotImplementedException();
//        }

//        public T GetService<T>()
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<object> GetServices(Type type)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<T> GetServices<T>()
//        {
//            throw new NotImplementedException();
//        }

//        private HttpContext GetHttpContext()
//        {
//            var contextAccessor = _rootServiceProvider.GetService<IHttpContextAccessor>();
//            if (contextAccessor == null)
//            {
//                throw new InvalidOperationException("没有初始化: IHttpContextAccessor");
//            }
//            return contextAccessor.HttpContext;
//        }
//    }

//    public static class ServiceLocatorHttpAdapterExtensions
//    {
//        public static IServiceCollection AddServiceLocatorHttpAdapter(this IServiceCollection services)
//        {
//            services.AddHttpContextAccessor();
//            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
//            services.AddSingleton<IServiceLocator, ServiceLocatorHttpAdapter>();
//            return services;
//        }

//        public static IApplicationBuilder UseServiceLocatorHttpAdapter(this IApplicationBuilder app)
//        {
//            ServiceLocator.Resolve = () => new ServiceLocatorHttpAdapter(app.ApplicationServices);
//            return app;
//        }
//    }
//}

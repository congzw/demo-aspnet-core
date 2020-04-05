//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;

//namespace Common
//{
//    //how to use:
//    //0 prepare => services.AddHttpContextAccessor();
//    //0 prepare => services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
//    //1 setup => services.AddSingleton<IServiceLocator, ServiceLocatorHttpAdapter>();
//    //2 use => ServiceLocator.Initialize(app.ApplicationServices.GetService<IServiceLocator>());
//    public class ServiceLocatorHttpAdapter : IServiceLocator
//    {
//        private readonly IServiceProvider _serviceProvider;

//        public ServiceLocatorHttpAdapter(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        public object GetService(Type type)
//        {
//            var httpContext = GetHttpContext();
//            if (httpContext == null)
//            {
//                return _serviceProvider.GetService(type);
//            }
//            return httpContext.RequestServices.GetService(type);
//        }

//        public IEnumerable<object> GetServices(Type type)
//        {
//            var httpContext = GetHttpContext();
//            if (httpContext == null)
//            {
//                return _serviceProvider.GetServices(type);
//            }
//            return httpContext.RequestServices.GetServices(type);
//        }

//        public T GetService<T>()
//        {
//            var httpContext = GetHttpContext();
//            if (httpContext == null)
//            {
//                return _serviceProvider.GetService<T>();
//            }
//            return httpContext.RequestServices.GetService<T>();
//        }

//        public IEnumerable<T> GetServices<T>()
//        {
//            var httpContext = GetHttpContext();
//            if (httpContext == null)
//            {
//                return _serviceProvider.GetServices<T>();
//            }
//            return httpContext.RequestServices.GetServices<T>();
//        }

//        private HttpContext GetHttpContext()
//        {
//            var contextAccessor = _serviceProvider.GetService<IHttpContextAccessor>();
//            if (contextAccessor == null)
//            {
//                throw new InvalidOperationException("没有初始化: IHttpContextAccessor");
//            }
//            return contextAccessor.HttpContext;
//        }
//    }
//}

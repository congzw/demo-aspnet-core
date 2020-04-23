using System;

namespace DemoDI.Demos.InitTasks
{
    public static class ServiceProviderExtensions
    {
        public static object GetFoo(this IServiceProvider provider, Type serviceType)
        {
            var supportFoo = provider as ISupportFoo;
            if (supportFoo != null)
            {
                return supportFoo.GetFoo(serviceType);
            }

            var service = provider.GetService(serviceType);
            if (service == null)
            {
                throw new InvalidOperationException("Bad!");
            }

            return service;
        }
    }
}

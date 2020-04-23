using System;

namespace DemoDI.Demos
{
    public interface IServiceProviderLocator
    {
        IServiceProvider GetProvider();
    }

    public sealed class ServiceProviderLocator : IServiceProviderLocator
    {
        public IServiceProvider GetProvider()
        {
            return Resolve == null ? NullServiceProvider.Instance : Resolve();
        }

        public static IServiceProviderLocator Instance = new ServiceProviderLocator();

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
}

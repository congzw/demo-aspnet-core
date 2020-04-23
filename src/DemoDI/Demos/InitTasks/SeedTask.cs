using System;

namespace DemoDI.Demos.InitTasks
{
    public class SeedTask
    {
        public static void Seed(IServiceProvider sp)
        {

        }
    }


    public interface ISupportFoo
    {
        object GetFoo(Type serviceType);
    }
}

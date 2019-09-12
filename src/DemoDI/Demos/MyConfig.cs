using System;

namespace DemoDI.Demos
{
    public class MyConfig : IDisposable
    {
        public MyConfig()
        {
            this.ReportCreate();
        }

        public void Dispose()
        {
            this.ReportDispose();
        }
    }
}
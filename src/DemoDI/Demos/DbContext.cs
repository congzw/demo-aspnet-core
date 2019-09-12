using System;

namespace DemoDI.Demos
{
    public class DbContext : IDisposable
    {
        public DbContext()
        {
            this.ReportCreate();
        }

        public void Dispose()
        {
            this.ReportDispose();
        }
    }
}
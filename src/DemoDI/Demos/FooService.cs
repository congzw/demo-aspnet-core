using System;

namespace DemoDI.Demos
{
    public class FooService : IDisposable
    {
        private readonly DbContext _dbContext;
        private readonly MyConfig _myConfig;

        public FooService(DbContext dbContext, MyConfig myConfig)
        {
            _dbContext = dbContext;
            _myConfig = myConfig;
            this.ReportCreate();
        }

        public void Dispose()
        {
            ////should not use with di!
            //_dbContext?.Dispose();
            this.ReportDispose();
        }
    }
}

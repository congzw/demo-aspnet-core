using System;

namespace DemoDI.Demos
{
    public class BarService : IDisposable
    {
        private readonly DbContext _dbContext;
        private readonly MyConfig _myConfig;
        private readonly FooService _fooService;

        public BarService(DbContext dbContext, MyConfig myConfig, FooService fooService)
        {
            _dbContext = dbContext;
            _myConfig = myConfig;
            _fooService = fooService;
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
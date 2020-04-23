using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoDI.Demos;
using DemoDI.Demos.InitTasks;
using DemoDI.Demos.ObjectTraces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DemoDI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooController : ControllerBase
    {
        private readonly FooService _fooService;
        private readonly BarService _barService;

        public FooController(FooService fooService, BarService barService)
        {
            _fooService = fooService;
            _barService = barService;
        }

        [Route("Get")]
        [HttpGet]
        public async Task<IEnumerable<string>> Get([FromServices]TraceDbContext dbContext)
        {
            LogHelper.Debug("<<<<scope sync");
            ServiceProviderLocator.Provider.RunInScope(x =>
            {
                var test = x.GetService<TraceDbContext>();
            });
            LogHelper.Debug("    scope sync>>>>");

            await Task.Run(() =>
            {
                LogHelper.Debug("<<<<scope async");
                ServiceProviderLocator.Provider.RunInScope(x =>
                {
                    var test = x.GetService<TraceDbContext>();
                });
                LogHelper.Debug("    scope async>>>>");
            });

            LogHelper.Debug("<<<<request");
            ServiceProviderLocator.Provider.GetService<TraceDbContext>();
            LogHelper.Debug("    request>>>>");
            
            var messages = ObjectCounter.Instance.Items;
            var results = messages.Select(x => string.Format("{0} {1}/{2}", x.Value.Type, x.Value.CurrentCount, x.Value.TotalCount));
            return results;
        }

        [Route("Clear")]
        [HttpGet]
        public string Clear()
        {
            var helper = ObjectCounter.Instance;
            helper.Items.Clear();
            return "Clear At: " + DateTime.Now.ToString("s");
        }
        
        [Route("StopLoop")]
        [HttpGet]
        public string StopLoop()
        {
            LoopTaskHelper.StopLoop("Foo");
            return "StopLoop At: " + DateTime.Now.ToString("s");
        }
    }
}

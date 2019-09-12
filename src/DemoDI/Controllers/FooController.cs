using System;
using System.Collections.Generic;
using System.Linq;
using DemoDI.Demos;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<string> Get()
        {
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
    }
}

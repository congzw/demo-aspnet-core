using System;
using Microsoft.AspNetCore.Mvc;
using MyWebApp.Common;

namespace MyWebApp.Apis
{
    [Route("api/Demo")]
    //[ApiController] //This make all actions can bind JSON 
    public class DemoApiController : ControllerBase
    {
        [Route("GetDate")]
        [HttpGet]
        public DateTime GetDate()
        {
            return DateTime.Now;
        }

        //This action can bind form data 
        [Route("Save")]
        [HttpPost]
        public MessageResult Save(DemoVo demoVo)
        {
            //TL;DR: Add the [FromBody] attribute to the parameter in your ASP.NET Core controller action Note,
            //if you're using ASP.NET Core 2.1, you can also use the [ApiController] attribute
            //to automatically infer the [FromBody] binding source for your complex action method parameters.

            //desc => https://andrewlock.net/model-binding-json-posts-in-asp-net-core/
            //Using the same HTTP requests as previously, we see the following console output,
            //where the x-www-url-formencoded POST is bound correctly,
            //but the JSON POST is not.
            return MessageResult.CreateSuccessResult("SaveOK", demoVo);
        }

        //This action can bind JSON 
        [Route("Save2")]
        [HttpPost]
        public MessageResult Save2([FromBody]DemoVo demoVo)
        {
            return MessageResult.CreateSuccessResult("Save2OK", demoVo);
        }
    }

    public class DemoVo
    {
        public string Name { get; set; }
    }
}

using System.Collections.Generic;
using JwtDemo.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtDemo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserApiController : ControllerBase
    {
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            //use postman header test:
            //Authorization: Bearer TheToken
            var users = new List<User>
            {
                new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test" }
            };
            return Ok(users);
        }
    }
}

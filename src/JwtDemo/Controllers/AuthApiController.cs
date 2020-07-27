using JwtDemo.AppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtDemo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth")]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthAppService _authAppService;

        public AuthApiController(IAuthAppService authAppService)
        {
            _authAppService = authAppService;
        }
        
        //use http get only for demo purpose!
        [AllowAnonymous]
        [HttpGet("AuthClient")]
        public AuthClientResult Login([FromQuery]AuthClientVo vo)
        {
            var authClientResult = _authAppService.AuthClient(vo);
            return authClientResult;
        }
    }
}

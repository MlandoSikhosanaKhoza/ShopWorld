using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWorld.BusinessLogic;
using ShopWorld.Shared;

namespace ShopWorld.API.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationLogic _authorizationLogic;
        public AuthorizationController(IAuthorizationLogic authorizationLogic) { 
            _authorizationLogic = authorizationLogic;
        }
        /// <summary>
        /// You can only login as a customer
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json",Type = typeof(LoginResult))]
        public IActionResult _Login(MobileLoginInputModel Input) {
            if (ModelState.IsValid)
            {
                return Ok(_authorizationLogic.Login(Input.MobileNumber));
            }
            return BadRequest(ModelState.Values);
        }

        /// <summary>
        /// You can only login as an admin
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", Type = typeof(LoginResult))]
        public IActionResult _LoginAsAdmin()
        {
            return Ok(_authorizationLogic.LoginAsAdmin());
        }
    }
}

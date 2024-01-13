using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizations.Services.Interfaces;

namespace Organizations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(string username, string password)
        {
            try
            {
                return Content(_accountService.RegisterUser(username, password));
                
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Registration failed", Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string username, string password)
        {
            try
            {
                string jwtToken = _accountService.LoginUser(username, password);

                if (jwtToken != null)
                {
                    return Ok(new { Token = jwtToken });
                }
                else
                {
                    return Unauthorized(new { Message = "Invalid username or password" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Login failed", Error = ex.Message });
            }
        }
    }
}

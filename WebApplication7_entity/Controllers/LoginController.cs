using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using WebApplication7_petPals.ApiStatusCode;
using WebApplication7_petPals.Models.Dto.UserDto;
using WebApplication7_petPals.Services.Login;

namespace WebApplication7_petPals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase

    {
        private readonly ILoginInterface _loginInterface;

        public LoginController(ILoginInterface loginInterface)
        {
            _loginInterface = loginInterface;
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            try
            {
                var token = await _loginInterface.Login(loginDto);
                var CookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(1)
                };
                Response.Cookies.Append("AuthorizationToken", token.Token, CookieOptions);
                var result = new ApiResponse<LoginResponseDto>(HttpStatusCode.OK, true, "loginSuccesfully",token);
                return Ok(result);  
                
            }
            catch (Exception ex)
            {
                return BadRequest("login failed "+ex.Message);

            }
        }
        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                Response.Cookies.Delete("AuthorizationToken");
                return Ok("logout succesfuly");
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
           
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                return Ok( token );


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

    }
}

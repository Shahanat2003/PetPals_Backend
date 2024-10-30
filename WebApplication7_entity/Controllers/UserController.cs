using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication7_petPals.Models.Dto.ProductDto;
using WebApplication7_petPals.Services.Users;

namespace WebApplication7_petPals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;
        public UserController(IUserInterface userInterface) {
        _userInterface = userInterface;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetUser")]
        
        public async Task<IActionResult> Getuser()
        {
            var user= await _userInterface.GetUser();
            return Ok(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("id:block/unblock")]
        public async Task<IActionResult> BlockUnblockUser(int user_id)
        {
            try
            {
                var result = await _userInterface.UserBlockedAndUnblocked(user_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

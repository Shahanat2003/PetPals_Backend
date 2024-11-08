using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using System.Net;
using WebApplication7_petPals.ApiStatusCode;
using WebApplication7_petPals.Models.Dto.ProductDto;
using WebApplication7_petPals.Models.Dto.UserDto;
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
        [HttpPut("blockUnblock")]
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
        [HttpGet]
        public async Task<IActionResult> getUserById(int userId)
        {
            try
            {
                var result =await  _userInterface.GetUserById(userId);
          
             
                return Ok(result);


            }
            catch(Exception ex)
            {
                return BadRequest("fetching failed" + ex.Message);

            }
        }
    }
}

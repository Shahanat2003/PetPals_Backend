﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication7_petPals.Models.Dto.UserDto;
using WebApplication7_petPals.Services.Register;

namespace WebApplication7_petPals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterInterface _registerInterface;
        public RegisterController(IRegisterInterface registerInterface)
        {
            _registerInterface = registerInterface;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            try
            {
                var s=await _registerInterface.Register(userRegisterDto);
                return Ok(s);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication7_petPals.Models;
using WebApplication7_petPals.Models.Dto.UserDto;

namespace WebApplication7_petPals.Services.Login
{
    public class LoginService : ILoginInterface
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public LoginService(AppDbContext appDbContext, IConfiguration configuration) {
            _context = appDbContext;
            _config = configuration;

        }

        public async Task<string> Login(UserLoginDto userDto) {
            try
            {
               
               
                    var us = await _context.Users.FirstOrDefaultAsync(x => x.Email == userDto.Email);
                    if (us == null || !ValidatePassword(userDto.Password, us.Password))
                    {
                        throw new InvalidOperationException("Invalid email or password");

                    }
                if (us.blocked)
                {
                    return "the user is blocked";
                }
                    
                
                    var token = CreateToken(us);
                    return token;
                
               
            }
           
            catch (Exception ex)
            {
                return ex.Message;

            }
        }


        private string CreateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim (ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim (ClaimTypes.Name,user.UserName),
                new Claim (ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: credentials,
                    expires: DateTime.Now.AddDays(1)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        private bool ValidatePassword(string password, string hashPassword) {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }


    }
}




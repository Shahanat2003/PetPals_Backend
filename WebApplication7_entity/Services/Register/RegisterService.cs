using AutoMapper;
using Microsoft.Identity.Client;
using WebApplication7_petPals.Models;
using WebApplication7_petPals.Models.Dto.UserDto;

namespace WebApplication7_petPals.Services.Register
{
    public class RegisterService:IRegisterInterface
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
       
        public RegisterService( IMapper mapper,AppDbContext dbContext)
        {
           _mapper = mapper;
            _appDbContext = dbContext;
           
           
        }
        public async Task<string> Register(UserRegisterDto UserDto)
        {
            try
            {
                if (UserDto == null)
                {
                    throw new ArgumentNullException("user cannot be null");
                }
                var existingUser = _appDbContext.Users.FirstOrDefault(u => u.Email == UserDto.Email);
                if (existingUser != null)
                {
                    return "the user already exist";
                }
                string hashPassword=BCrypt.Net.BCrypt.HashPassword(UserDto.Password);
                var user = _mapper.Map<User>(UserDto);
                user.Password = hashPassword;
                _appDbContext.Users.Add(user);
                await _appDbContext.SaveChangesAsync();
                return "item added succesfuly";

            }
            catch (Exception ex) { 
            return(ex.Message);
            }
            
            
                
            }
        }
    }


using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using WebApplication7_petPals.Models.Dto.OrderDto;
using WebApplication7_petPals.Models.Dto.UserDto;

namespace WebApplication7_petPals.Services.Users
{
    public class UserService : IUserInterface
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
       
        public UserService(AppDbContext appDbContext,IMapper mapper) { 
        _appDbContext = appDbContext;
            _mapper = mapper;
           
        
        }
        public async Task<List<OutputUserDto>> GetUser()
        {
            var u=await _appDbContext.Users.ToListAsync();
            return _mapper.Map<List<OutputUserDto>>(u);
        }
        public async Task<string> UserBlockedAndUnblocked(int user_id)
        {
            try
            {
                var user = _appDbContext.Users.SingleOrDefault(u => u.UserId == user_id);
                if (user != null)
                {
                    user.blocked = !user.blocked;
                    _appDbContext.SaveChanges();
                    return user.blocked == true ? "the user is blocked" : "user is unblocked";
                }
                return "the user is invalid";

            }
            catch (Exception ex)
            {
                return "the error is occured when trying to bloked/unblock the user";
                throw new Exception(ex.Message);
            }

        }
        public async Task<OutputUserDto> GetUserById(int user_id)
        {
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserId == user_id);
                if (user == null)
                {
                    throw new Exception("invalid user");
                }
               return   _mapper.Map<OutputUserDto>(user);
              

            }catch(Exception ex)
            {
                throw new Exception("error occured fetching user" + ex.Message);

            }
        }






    }
}

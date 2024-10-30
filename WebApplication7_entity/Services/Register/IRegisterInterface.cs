using WebApplication7_petPals.Models.Dto.UserDto;

namespace WebApplication7_petPals.Services.Register
{
    public interface IRegisterInterface
    {
        Task<string> Register(UserRegisterDto UserDto);
       
    }
}

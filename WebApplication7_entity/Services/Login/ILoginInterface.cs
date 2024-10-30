using WebApplication7_petPals.Models.Dto.UserDto;

namespace WebApplication7_petPals.Services.Login
{
    public interface ILoginInterface
    {
        Task<string> Login(UserLoginDto userLoginDto);
    }
}

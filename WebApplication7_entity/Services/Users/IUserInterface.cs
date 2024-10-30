using System.Net.NetworkInformation;
using WebApplication7_petPals.Models;
using WebApplication7_petPals.Models.Dto.UserDto;

namespace WebApplication7_petPals.Services.Users
{
    public interface IUserInterface
    {
        Task<List<OutputUserDto>> GetUser();
        Task<string> UserBlockedAndUnblocked(int user_id);
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApplication7_petPals.Models.Dto.UserDto
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApplication7_petPals.Models.Dto.UserDto
{
    public class UserRegisterDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}

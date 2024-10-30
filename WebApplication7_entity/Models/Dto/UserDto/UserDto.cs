namespace WebApplication7_petPals.Models.Dto.UserDto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool Blocked { get; set; }
    }
}

namespace WebApplication7_petPals.Models.Dto.UserDto
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Role {  get; set; }
        public string Token { get; set; }
    }
}

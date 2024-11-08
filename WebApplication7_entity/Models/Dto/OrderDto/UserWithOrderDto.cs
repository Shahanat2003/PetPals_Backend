namespace WebApplication7_petPals.Models.Dto.OrderDto
{
    public class UserWithOrderDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<OrderViewDto> Orders { get; set; }
    }
}

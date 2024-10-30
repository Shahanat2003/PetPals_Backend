using WebApplication7_petPals.Models.Dto.OrderDto;

namespace WebApplication7_petPals.Services.Orders
{
    public interface IOrderInterface
    {
        public Task<bool> RazorPaycreate(string token,OrderRequestDto orderRequestDto);
        Task<string> RazorPayPayment(long price);
        Task<List<OrderViewDto>> GetOrders(int userId);
        public bool payment(RazorPayDto razorPayDto);
    }
}

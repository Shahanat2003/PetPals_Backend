using WebApplication7_petPals.Models.Dto.OrderDto;

namespace WebApplication7_petPals.Services.Orders
{
    public interface IOrderInterface
    {
        public Task<bool> RazorPaycreate(int user_id,OrderRequestDto orderRequestDto);
        Task<string> RazorOrderIdCreate(long price);
        Task<List<OrderViewDto>> GetOrders(int user_id);

        public bool payment(RazorPayDto razorPayDto);
        public  Task<List<OrderAdminDto>> OrderAdminView();
        public Task<decimal> TotalRevenue();
    }
}

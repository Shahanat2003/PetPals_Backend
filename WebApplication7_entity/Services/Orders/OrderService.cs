using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using WebApplication7_petPals.Models;
using WebApplication7_petPals.Models.Dto.OrderDto;
using WebApplication7_petPals.Services.JWT_id;

namespace WebApplication7_petPals.Services.Orders
{
    public class OrderService:IOrderInterface
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly string _hostUrl;
        private readonly IJwtIdInterface _jwtIdInterface;
        public OrderService(IConfiguration configuration,IJwtIdInterface jwtIdInterface,AppDbContext appDbContext,IMapper mapper) {
        _configuration = configuration;
            _jwtIdInterface = jwtIdInterface;
            _context = appDbContext;
            _hostUrl = _configuration["HostUrl:url"];
            _mapper = mapper;
        }
        public async Task<bool> RazorPaycreate(string token,OrderRequestDto orderRequestDto)
        {
            var user_id=_jwtIdInterface.GetUserFromToken(token);
            if (user_id == 0) throw new Exception("the user with the given token is invalid");
            if(orderRequestDto.Transaction_id==null&&orderRequestDto.Order_string==null)
            {
                return false;
            }
            var user=await _context.carts.Include(c=>c.CartItems).ThenInclude(p=>p.Product).FirstOrDefaultAsync(u=>u.User_id==user_id);
            var order = new Models.Order
            {
                User_id = user_id,
                Customer_Name = orderRequestDto.Customer_name,
                Customer_address = orderRequestDto.Customer_address,
                Customer_city = orderRequestDto.Customer_city,
                Customer_phone = orderRequestDto.Customer_phone,
                Customer_email = orderRequestDto.Customer_email,
                Order_date = DateTime.Now,
                Order_string = orderRequestDto.Order_string,
                Transaction_id = orderRequestDto.Transaction_id,
                OrderItems = user.CartItems.Select(oi => new OrderItem
                {
                    Product_id = oi.Product_id,
                    Total_Price = oi.Quantity * oi.Product.NewPrice,
                    quantity = oi.Quantity
                }).ToList()

            };
            _context.orders.Add(order);
            _context.carts.Remove(user);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<string> RazorPayPayment(long price)
        {
            try
            {
                Dictionary<string, object> input = [];
                Random random = new();
                string TransactionId = random.Next(0, 100).ToString();
                input.Add("amount", Convert.ToDecimal(price) * 100);
                input.Add("currency", "INR");
                input.Add("receipt", TransactionId);

                string key = _configuration["RazorPay:KeyId"];
                string secretKey = _configuration["RazorPay:KeySecret"];
                RazorpayClient client = new RazorpayClient(key, secretKey);
                Razorpay.Api.Order order = client.Order.Create(input);
                var OrderId = order["id"].ToString();
                return await Task.FromResult(OrderId);

            }
            catch (Exception ex) {
                return null;
                throw new Exception("an exeption is occure in Razor pay"+ex.Message);
            }
        }
        public async Task<List<OrderViewDto>> GetOrders(int userId)
        {
            try
            {
                var order = await _context.orders.Include(c => c.OrderItems).Where(c => c.User_id == userId).ToListAsync();
                if (order.Count > 0)
                {
                    return _mapper.Map<List<OrderViewDto>>(order);
                }
                return new List<OrderViewDto>();
            }
            catch (Exception ex) { 
            return new List<OrderViewDto>();
                throw new Exception("error occure try to get orders");
            }

        }
        public bool payment(RazorPayDto razorPayDto)
        {
            if (razorPayDto == null ||
                razorPayDto.Razor_payId == null ||
                razorPayDto.Razor_Sign == null ||
                razorPayDto.Razor_OrderId == null)
                return false;
            RazorpayClient razorpay = new RazorpayClient(_configuration["RazorPay:KeyId"], _configuration["RazorPay: KeySecret"]);
            Dictionary<string, string> attributes = [];
            attributes.Add("Razorpay_paymentId", razorPayDto.Razor_payId);
            attributes.Add("Razorpay_orderId", razorPayDto.Razor_OrderId);
            attributes.Add("Razorpay_signature", razorPayDto.Razor_Sign);
            Utils.verifyPaymentLinkSignature(attributes);
            return true;

        }
    }
}

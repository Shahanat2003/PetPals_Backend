using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
        public async Task<bool> RazorPaycreate(int user_id,OrderRequestDto orderRequestDto)
        {
            try
            {
                //var user_id = _jwtIdInterface.GetUserFromToken(to);
                //if (user_id == 0) throw new Exception("the user with the given token is invalid");
                //if (orderRequestDto.Transaction_id == null && orderRequestDto.Order_string == null)
                //{
                //    return false;
                //}

                var cart = await _context.carts.Include(c => c.CartItems).ThenInclude(p => p.Product).FirstOrDefaultAsync(u => u.User_id == user_id);
                if (cart==null) { 
                return false;
                }
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
                    OrderItems = cart.CartItems.Select(oi => new OrderItem
                    {
                        Product_id = oi.Product_id,
                        Total_Price = oi.Quantity * oi.Product.NewPrice,
                        quantity = oi.Quantity
                    }).ToList()
                        
                };
                _context.orders.Add(order);
                _context.carts.Remove(cart);
                await _context.SaveChangesAsync();
                return true;


            }
            catch (Exception ex) { 
                throw new Exception(ex.Message);
            
            }
         
        }
        public async Task<string> RazorOrderIdCreate(long price)
        {
            try
            {
                Dictionary<string, object> input = new Dictionary<string, object>();
                Random random = new Random();
                string TransactionId = random.Next(0, 100).ToString();
                input.Add("amount", Convert.ToDecimal(price) * 100);
                input.Add("currency", "INR");
                input.Add("receipt", TransactionId);

                string key = _configuration["RazorPay:KeyId"];
                string secretKey = _configuration["RazorPay:KeySecret"];
                RazorpayClient client = new RazorpayClient(key, secretKey);
                Razorpay.Api.Order order = client.Order.Create(input);
                var OrderId = order["id"].ToString();
                return OrderId;

            }
            catch (Exception ex) {
                
                throw new Exception("an exeption is occure in Razor pay"+ex.Message);
            }
        }
  
        public bool payment(RazorPayDto razorPayDto)
        {
            if (razorPayDto == null ||
                razorPayDto.Razor_payId == null ||
                razorPayDto.Razor_Sign == null ||
                razorPayDto.Razor_OrderId == null)
                return false;
            try
            {
                RazorpayClient razorpay = new RazorpayClient(_configuration["RazorPay:KeyId"], _configuration["RazorPay: KeySecret"]);
                Dictionary<string, string> attributes = [];
                attributes.Add("Razorpay_paymentId", razorPayDto.Razor_payId);
                attributes.Add("Razorpay_orderId", razorPayDto.Razor_OrderId);
                attributes.Add("Razorpay_signature", razorPayDto.Razor_Sign);
                Utils.verifyPaymentLinkSignature(attributes);
                return true;

            }
            catch (Exception ex) {
                return false;
                throw new Exception(ex.Message);


            }
        }
        public async Task<List<OrderViewDto>> GetOrders(int user_id)
        {
            var orders = await _context.orders.Include(oi => oi.OrderItems).ThenInclude(p => p.Product).Where(u => u.User_id == user_id).ToListAsync(); 
            var orderDetails=new List<OrderViewDto>();
            foreach (var order in orders)
            {
                foreach (var item in order.OrderItems) {
                    var orderDeatilss = new OrderViewDto
                    {
                        Id = item.Id,
                        orderDate = order.Order_date,
                        Product_Name = item.Product.Name,
                        image = _hostUrl+item.Product.Img,
                        quantity = item.quantity,
                        totalPrice = item.Total_Price,
                        order_string = order.Order_string
                    };
                    orderDetails.Add(orderDeatilss);
                }
              

            }

            return orderDetails;
        }

        public async Task<List<OrderAdminDto>> OrderAdminView()
        {
            var orders = await _context.orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                
                .ToListAsync();

            if (orders.Count > 0)
            {
               
                var orderDetails = orders.Select(o => new OrderAdminDto
                {
                    Customer_Id = o.Id,
                    CustomerName = o.Customer_Name,
                    CustomerEmail = o.Customer_email,
                    CustomerAddres = o.Customer_address,
                    OrderId = o.Order_string,
                    Order_Date = o.Order_date,
                    TransactionId = o.Transaction_id,
                    Orders = o.OrderItems.Select(oi => new OrderViewDto
                    {
                        Id = oi.Id,
                        Product_Name = oi.Product.Name,
                        quantity = oi.quantity,
                        totalPrice = oi.Total_Price,
                        image = _hostUrl+ oi.Product.Img,
                        orderDate = o.Order_date,
                        order_string = o.Order_string
                    }).ToList()
                }).ToList();

                return orderDetails;
            }
            return new List<OrderAdminDto>();
        }
        public async Task<decimal> TotalRevenue()
        {
            try
            {
                var total = await _context.orderItems.SumAsync(o => o.Total_Price);
                return total;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);

            }
           

        }
    }
}

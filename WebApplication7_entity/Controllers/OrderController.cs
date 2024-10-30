using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication7_petPals.Models.Dto.OrderDto;
using WebApplication7_petPals.Services.Orders;

namespace WebApplication7_petPals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderInterface _orderInterface;
        public OrderController(IOrderInterface orderInterface)
        {
            _orderInterface = orderInterface;
        }

        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrder(OrderRequestDto orderRequestDto)

        {
            if (orderRequestDto == null)
            {
                return BadRequest();
            }
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var jwtToken = token?.Split(' ')[1];

            if (orderRequestDto == null || jwtToken == null)
            {
                return BadRequest();

            }
            var result = await _orderInterface.RazorPaycreate(token, orderRequestDto);
            return Ok(result);
        }
        [HttpPost("RazorPayment")]
        public async Task<IActionResult> RazorPayment(long price)
        {
            if (price <= 0 || price > 100000)
            {
                return BadRequest("enter valid amount");
            }
            var result = await _orderInterface.RazorPayPayment(price);
            return Ok(result);

        }
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders(int userId)
        {
            try
            {
                var result = await _orderInterface.GetOrders(userId);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult paymnet(RazorPayDto razorPayDto)
        {
            if (razorPayDto == null)
            {
                return BadRequest("razor pay details is null");
            }
            var result = _orderInterface.payment(razorPayDto);
            return Ok(result);
        }
    }
}



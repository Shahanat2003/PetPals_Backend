using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication7_petPals.ApiStatusCode;
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
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderRequestDto orderRequestDto)

        {
            
            //var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            //var jwtToken = token?.Split(' ')[1];

            //if (orderRequestDto == null || jwtToken == null)
            //{
            //    return BadRequest();

            //}
            try
            {
                if (orderRequestDto == null)
                {
                    return BadRequest();
                }
                var userId = GetUseid();
                var response = await _orderInterface.RazorPaycreate(userId, orderRequestDto);
                return Ok(response);
            }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
            }
           
        }
        [HttpPost("RazorIdCreate")]
        [Authorize]
        public async Task<IActionResult> RazorOrderIdCreate(long price)
        {
            try
            {
                if (price <= 0 || price > 100000)
                {
                    return BadRequest("enter valid amount");
                }
                var response = await _orderInterface.RazorOrderIdCreate(price);
                return Ok(response);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
           

        }
        [HttpGet("GetOrders")]
        [Authorize]
        public async Task<IActionResult> GetOrders(int userId)
        {
            try
            {
                var response = await _orderInterface.GetOrders(userId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Authorize]
        public  IActionResult Paymnet(RazorPayDto razorPayDto)
        {
            try
            {
                if (razorPayDto == null)
                {
                    return BadRequest("razor pay details is null");
                }
                var response = _orderInterface.payment(razorPayDto);
                return Ok(response);

            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet("AdminView")]
        public async Task<IActionResult> OrderAdminView()
        {
            try
            {
                var result =await _orderInterface.OrderAdminView();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("totalRevenue")]
        public async Task<IActionResult> TotalRevenue()
        {
            try
            {
                var result = await _orderInterface.TotalRevenue();
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private int GetUseid()
        {
            var userIdString=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(int.TryParse(userIdString,out int userId))
            {
                return userId;
            }
            throw new Exception("invalid user");
        }

      
    }
}



using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication7_petPals.Services.Cart;

namespace WebApplication7_petPals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase

    {

        private readonly ICartInterface _cart;
        public CartController(ICartInterface cartInterface)
        {
            _cart = cartInterface;
        }
        [Authorize (Roles ="User")]
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddItem(int productId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

                // Check if token is missing or in an unexpected format
                if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
                {
                    Console.WriteLine("Received token: " + token); // Log to check the token
                    return BadRequest("Authorization token is missing or invalid.");
                }

                var jwtToken = token.Split(' ')[1];
                var result = await _cart.AddToCart(productId, jwtToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetCartItem()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var splitToken = token?.Split(' ');
            var jwtToken = splitToken?[1];
            var result=await _cart.GetCartItem(jwtToken);
            return Ok(result);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> RemoveCart(int productId)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var jwtToken= token?.Split(' ')[1];
            
            var result=await _cart.RemoveCart(productId,jwtToken);
            return Ok(result);

        }
        [Authorize(Roles = "User")]
        [HttpPut("id:increseQuantity")]
        public async Task<IActionResult> IncreaseQunatity(int productId)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var jwtToken=token?.Split(' ')[1];
            var result=await _cart.IncreaseQuantity(productId,jwtToken);
            return Ok(result);
        }
        [Authorize(Roles = "User")]
        [HttpPut("id:Decrease:Quantity")]
        public async Task<IActionResult> DecreseQuantity(int productId)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var jwtToken = token?.Split(' ')[1];
            var result=await _cart.DecreaseQuantity(productId,jwtToken);
            return Ok(result);

        }
    }
   
}

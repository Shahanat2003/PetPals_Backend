using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using System.Net;
using System.Security.Claims;
using WebApplication7_petPals.ApiStatusCode;
using WebApplication7_petPals.Models.CartDto;
using WebApplication7_petPals.Models.Dto.UserDto;
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
                //var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

                // Check if token is missing or in an unexpected format
                //if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
                //{
                //    Console.WriteLine("Received token: " + token); // Log to check the token
                //    return BadRequest("Authorization token is missing or invalid.");
                //}

                //var jwtToken = token?.Split(' ')[1];
                var userid = GetingUserIdByClaims();
                
                var result = await _cart.AddToCart(productId, userid);
                if(result== "item already in the cart")
                {
                    var res = new ApiResponse<string>(HttpStatusCode.Conflict, false, "Item already in the cart", null);
                    return Conflict(res);
                }
                var response = new ApiResponse<string>(HttpStatusCode.OK, true, "item added to cart", result);
                return Ok(response);

               
            }
            catch (Exception ex)
            {
                return Unauthorized("unAuthorized or invalid token");
            }
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetCartItem()
        {
            //var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            //var splitToken = token?.Split(' ');
            //var jwtToken = splitToken?[1];
            try
            {
                var userid = GetingUserIdByClaims();
                var response = await _cart.GetCartItem(userid);


                return Ok(response);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [Authorize(Roles = "User")]
        [HttpDelete("id")]
        public async Task<IActionResult> RemoveCart(int productId)
        {
            //var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            //var jwtToken= token?.Split(' ')[1];

            var userid = GetingUserIdByClaims();
            var response=await _cart.RemoveCart(productId, userid);
            var result = new ApiResponse<bool>(HttpStatusCode.OK, true,"item removed", response);
            return Ok(result);

        }
        [Authorize(Roles = "User")]
        [HttpPut("IncreaseQuantity")]
        public async Task<IActionResult> IncreaseQuantity(int productId)
        {
            var userid = GetingUserIdByClaims();
            var respons = await _cart.IncreaseQuantity(productId, userid);
            var result = new ApiResponse<string>(HttpStatusCode.OK, true, "qunatityIncresed", respons);
            return Ok(result);
        }


        [Authorize(Roles = "User")]
        [HttpPut("DecreaseQuantity")]
        public async Task<IActionResult> DecreseQuantity(int productId)
        {
            //var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            //var jwtToken = token?.Split(' ')[1];
            var userid = GetingUserIdByClaims();
            var response=await _cart.DecreaseQuantity(productId, userid);
            var result = new ApiResponse<string>(HttpStatusCode.OK, true, "quantityDecresed", response);
            return Ok(result);

        }

        private int GetingUserIdByClaims()
        {
            var ClaimOfId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(int.TryParse(ClaimOfId,out int user_id))
            {
                return user_id;
            }
            throw new Exception("Invalid User Id");

        }
    }
   
}

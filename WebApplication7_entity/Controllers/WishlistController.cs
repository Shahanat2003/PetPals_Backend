using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication7_petPals.Services.Wishlist;

namespace WebApplication7_petPals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistInterface _wishlists;
        public WishlistController(IWishlistInterface wishlists)
        {
            _wishlists = wishlists;
        }
        [Authorize(Roles = "User")]
        [HttpPost("addToWishlist")]
        public async Task<IActionResult> AddtoWishlist( int product_id)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var jwtToken = token?.Split(' ')[1];
            var itemExist = await _wishlists.AddToWishlist(jwtToken, product_id);
           
            return Ok(itemExist);
        }
        //[HttpDelete("id")]
        //public async Task<IActionResult> RemoveWishlist(int product_id)
        //{
        //    var result=await _wishlists.RemoveFromWishlist(product_id);
        //    return Ok(result);

        //}
        [Authorize(Roles = "User")]
        [HttpGet("GetWishlistItems")]
        public async Task<IActionResult>GetWishlist()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var JwtToken=token?.Split(" ")[1];
                var result = await _wishlists.GetWishList(JwtToken);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
            }

    }
}

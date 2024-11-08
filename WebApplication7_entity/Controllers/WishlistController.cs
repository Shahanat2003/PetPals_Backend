using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using WebApplication7_petPals.ApiStatusCode;
using WebApplication7_petPals.Models;
using WebApplication7_petPals.Models.Dto.WishlistDto;
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
            var userId = GetUserIdByClaims();
            //var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            //var jwtToken = token?.Split(' ')[1];
            var itemExist = await  _wishlists.AddToWishlist(userId, product_id);
            var result =new ApiResponse<string>(HttpStatusCode.OK, true, "wishlist", itemExist);


           
            return Ok(result);
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
                //var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                //var JwtToken=token?.Split(" ")[1];
                var userId = GetUserIdByClaims();

                var result = await _wishlists.GetWishList(userId);
                var response = new ApiResponse<List<WhishlistOutDto>>(HttpStatusCode.OK, true, "wishlist", result);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
            }

        private int GetUserIdByClaims()
        {
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(int.TryParse(userClaim,out int userId))
            {
                return userId;
            }
            throw new Exception("invalid user id");
        }

    }
}

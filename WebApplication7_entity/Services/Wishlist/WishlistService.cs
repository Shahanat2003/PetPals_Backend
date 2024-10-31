using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using WebApplication7_petPals.Migrations;
using WebApplication7_petPals.Models;
using WebApplication7_petPals.Models.Dto.WishlistDto;
using WebApplication7_petPals.Services.JWT_id;

namespace WebApplication7_petPals.Services.Wishlist
{
    public class WishlistService:IWishlistInterface
    {
        private readonly AppDbContext _context;
        private readonly IJwtIdInterface _jwtIdInterface;
        private readonly IMapper _mapper;
        public WishlistService(AppDbContext appDbContext,IMapper mapper,IJwtIdInterface jwtIdInterface) { 
        _context = appDbContext;
            _mapper = mapper;
            _jwtIdInterface = jwtIdInterface;
        }

        public async Task<string> AddToWishlist(int userId, int product_id)
        {
            try
            {
                //var user_id=_jwtIdInterface.GetUserFromToken(token);
                if (userId == null) {
                    return "invalid userId";
                }

                //var existingItem = await _context.wishlists.AnyAsync(c => c.User_id == user_id && c.Product_id == product_id);
                var existingItem = await _context.wishlists.Include(w => w.Product).FirstOrDefaultAsync(p => p.User_id == userId && p.Product_id == product_id);
                if (existingItem==null)
                {
                    var WishlistDto = new WishlistDto()
                    {
                        Product_id = product_id,
                        User_id = userId
                    };
                    var newItem=_mapper.Map<Wishlists>(WishlistDto);
                    _context.wishlists.Add(newItem);
                    await _context.SaveChangesAsync();
                    return "product added to the wishlist";
                }
                _context.wishlists.Remove(existingItem);
                await _context.SaveChangesAsync();

                return "item removed from wishlist";

            }
            catch (Exception ex) {
                return ex.Message;
                throw new Exception("error occuring when product add to wishlist");

            }
           
        }
       //public async Task<bool> RemoveFromWishlist(int product_id)
       // {
       //     try
       //     {
       //         var item = await _context.wishlists.FirstOrDefaultAsync(p => p.Product_id == product_id);
       //         if (item != null)
       //         {
       //             _context.wishlists.Remove(item);
       //             await _context.SaveChangesAsync();
       //             return true;
       //         }
       //         return false; ;

       //     }
       //     catch (Exception ex) { 
       //         return false;

       //     }
            

       // }
        public async Task<List<WhishlistOutDto>> GetWishList(int userId)
        {
            try
            {
                //var userId = _jwtIdInterface.GetUserFromToken(token);
                var wishlist = await _context.wishlists.Include(c => c.Product).ThenInclude(p => p.Category).Where(w => w.User_id == userId).ToListAsync();
                if (wishlist.Count > 0)
                {
                    return wishlist.Select(p => new WhishlistOutDto
                    {
                        Id = p.Product.Id,
                        Product_Name = p.Product.Name,
                        price = p.Product.NewPrice,
                        Product_Description = p.Product.Description,
                        Catogory = p.Product.Category.Name,
                        Image = p.Product.Img
                    }).ToList();

                }
                else
                {
                    return new List<WhishlistOutDto>();
                }

            }
            catch (Exception ex) {
                return new List<WhishlistOutDto>();
                throw new Exception(ex.Message);               
            }
           
        }
    }
}

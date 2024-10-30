﻿using WebApplication7_petPals.Models.Dto.WishlistDto;

namespace WebApplication7_petPals.Services.Wishlist
{
    public interface IWishlistInterface
    {
        Task<string> AddToWishlist(string token, int product_id);
        //Task<bool> RemoveFromWishlist(int product_id);
        Task<List<WhishlistOutDto>> GetWishList(string token);
    }
}

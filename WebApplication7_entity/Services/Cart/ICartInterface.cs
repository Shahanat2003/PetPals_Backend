﻿using WebApplication7_petPals.Models.CartDto;

namespace WebApplication7_petPals.Services.Cart
{
    public interface ICartInterface
    {
        Task<List<OutCartDto>> GetCartItem(string token);
        Task<bool> AddToCart(int product_id, string token);
        Task<bool> RemoveCart(int product_id,string token);
        Task<bool> IncreaseQuantity(int product_id, string token);
        Task<string> DecreaseQuantity(int product_id, string token);

    }
}
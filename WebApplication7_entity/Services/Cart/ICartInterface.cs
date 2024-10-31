using WebApplication7_petPals.Models.CartDto;

namespace WebApplication7_petPals.Services.Cart
{
    public interface ICartInterface
    {
        Task<List<OutCartDto>> GetCartItem(int userId);
        Task<bool> AddToCart(int product_id, int userId);
        Task<bool> RemoveCart(int product_id, int userId);
        Task<bool> IncreaseQuantity(int product_id, int userId);
        Task<string> DecreaseQuantity(int product_id, int userId);

    }
}

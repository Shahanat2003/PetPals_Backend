using Microsoft.EntityFrameworkCore;
using WebApplication7_petPals.Models;
using WebApplication7_petPals.Models.CartDto;
using WebApplication7_petPals.Services.JWT_id;

namespace WebApplication7_petPals.Services.Cart
{
    public class CartServices : ICartInterface
    {
        private readonly AppDbContext _context;
        private readonly IJwtIdInterface _jwtIdInterface;
        private readonly IConfiguration _configuration;
        private readonly string _hostURL;

        public CartServices(AppDbContext context, IJwtIdInterface jwtIdInterface, IConfiguration configuration)
        {
            _context = context;
            _jwtIdInterface = jwtIdInterface;
            _configuration = configuration;
            _hostURL = _configuration["HostUrl:url"];
        }
        public async Task<bool> AddToCart(int product_id, int userId)
        {
            try
            {
                //var user_id = _jwtIdInterface.GetUserFromToken(token);
                if (userId == 0) throw new Exception("user no valid");
                var user = await _context.Users.Include(u => u.Cart)
                    .ThenInclude(c => c.CartItems)
                    .FirstOrDefaultAsync(u => u.UserId == userId);
                if (user == null) throw new Exception("not found the user");
                var product = await _context.products.FirstOrDefaultAsync(p => p.Id == product_id);
                if (product == null) throw new Exception($"product is not found with id of {product_id}");
                if (user != null && product != null)
                {
                    if (user.Cart == null)
                    {
                        user.Cart = new Models.Cart()
                        {
                            User_id = userId,
                            CartItems = new List<CartItem>()
                        };
                        _context.carts.Add(user.Cart);
                        await _context.SaveChangesAsync();

                    }
                   
                }
                CartItem existingProduct = user.Cart.CartItems.FirstOrDefault(c => c.Product_id == product_id);
                if (existingProduct != null)
                {
                    existingProduct.Quantity++;
                }
                else
                {
                    CartItem cartItem = new CartItem
                    {
                        Cart_id = user.Cart.Id,
                        Product_id = product_id,
                        Quantity = 1
                    };
                    _context.cartItems.Add(cartItem);
                }
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public async Task<List<OutCartDto>> GetCartItem(int userId)
        {
            try
            {
                //int user_id = _jwtIdInterface.GetUserFromToken(token);
                if (userId == 0) throw new Exception("the user is invalid");
                var user = await _context.carts.Include(p => p.CartItems).ThenInclude(c => c.Product).FirstOrDefaultAsync(po => po.User_id == userId);
                if (user == null) throw new Exception("no user found");
                if (user != null)
                {
                    var cartItem = user.CartItems.Select(c => new OutCartDto
                    {
                        Id = c.Product.Id,
                        ProductName = c.Product.Name,
                        Price = c.Product.NewPrice,
                        Quantity = c.Quantity,
                        TotalPrice = c.Product.NewPrice * c.Quantity,
                        Image = _hostURL + c.Product.Img
                    }).ToList();
                    return cartItem;
                }
                return new List<OutCartDto>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> RemoveCart(int product_id, int userId)
        {
            try
            {
                //var userId = _jwtIdInterface.GetUserFromToken(token);
                if (userId == 0) throw new Exception("invalid user");
                var user = await _context.Users.Include(c => c.Cart).ThenInclude(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
                if (user == null) throw new Exception("error of fething user");
                var product = await _context.products.FirstOrDefaultAsync(p => p.Id == product_id);
                if (product == null) throw new Exception("error when fetching product");
                if (user != null && product != null)
                {
                    var removeItem = user.Cart.CartItems.FirstOrDefault(c => c.Product_id == product_id);
                    if (removeItem != null)
                    {
                        _context.cartItems.Remove(removeItem);
                        _context.SaveChanges();
                        return true;
                    }


                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);

            }

        }
        public async Task<bool> IncreaseQuantity(int product_id, int userId)
        {
            try
            {
                //var user_id = _jwtIdInterface.GetUserFromToken(token);
                var user = await _context.Users.Include(c => c.Cart).ThenInclude(p => p.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
                if (user == null) throw new Exception("canot find the user");
                var product = await _context.products.FirstOrDefaultAsync(p => p.Id == product_id);
                if (product == null) throw new Exception(" invalid the product");
                if (user != null && product != null)
                {
                    var item = user.Cart.CartItems.FirstOrDefault(p => p.Product_id == product_id);
                    if (item != null)
                    {
                        item.Quantity++;
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    return false;

                }
                return false;

            }
            catch (Exception ex) {
                return false;
                throw new Exception("error when increase the quantity");
            }
           
        }
        public async Task<string> DecreaseQuantity(int product_id, int userId)
        {
            try
            {
                //var userId = _jwtIdInterface.GetUserFromToken(token);
                if (userId == null) throw new Exception("invalid token for the id");
                var user = await _context.Users.Include(c => c.Cart).ThenInclude(p => p.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
                var product = await _context.products.FirstOrDefaultAsync(p => p.Id == product_id);
                if (product == null) throw new Exception("invalid product id");
                if (user != null && product != null)
                {
                    var item = user.Cart.CartItems.FirstOrDefault(p => p.Product_id == product_id);
                    if (item != null)
                    {
                        item.Quantity--;
                        await _context.SaveChangesAsync();
                        return "quantity is decreased";
                    }
                    return "the item is not found in the user";
                }
                return "error of fetching product and user";

            }
            catch (Exception ex) { 
            return ex.Message;
            }
          
        }
    }
}

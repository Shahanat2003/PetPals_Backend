using WebApplication7_petPals.Models.Dto.ProductDto;

namespace WebApplication7_petPals.Services.Products
{
    public interface IProductInterface
    {
        Task<List<OutPrdctDto>> GetAllProducts();
        Task<List<OutPrdctDto>> GetProductById(int id);
        Task<List<OutPrdctDto>> GetProductByName(string name);
        Task<bool> CreateProduct(CreatePrdctDto productDto, IFormFile image);

        Task<string> UpdateProduct(int id, CreatePrdctDto productDto,IFormFile image);
     
        Task<bool> DeleteProduct(int id);
    }
}

﻿using WebApplication7_petPals.Models.Dto.ProductDto;

namespace WebApplication7_petPals.Services.Products
{
    public interface IProductInterface
    {
        Task<List<OutPrdctDto>> GetAllProducts();
        Task<List<OutPrdctDto>> GetProductById(int id);
        Task<List<OutPrdctDto>> GetProductByName(string Catogry_name);
        Task<string> CreateProduct(CreatePrdctDto productDto, IFormFile image);

        Task<string> UpdateProduct(int id, CreatePrdctDto productDto,IFormFile image);
     
        Task<bool> DeleteProduct(int id);

        Task<List<OutPrdctDto>> Searchproduct(string search);
        Task<List<OutPrdctDto>> Pagination(int page, int pageSize);

    }
}

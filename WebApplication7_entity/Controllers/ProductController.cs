using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication7_petPals.Models.Dto.ProductDto;
using WebApplication7_petPals.Services.Products;
using WebApplication7_petPals.Services.Users;

namespace WebApplication7_petPals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductInterface _productInterface;
        public ProductController(IProductInterface productInterface)
        {
            _productInterface = productInterface;
        }
        [Authorize]
        [HttpGet("AllProduct")]
        public async Task<IActionResult> GetAll()
        {
            var result=await _productInterface.GetAllProducts();
            return Ok(result);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> PostPoduct([FromForm]CreatePrdctDto prdctDto,IFormFile image)
        {
            var result = await _productInterface.CreateProduct(prdctDto,image);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("id")]
        public async Task<IActionResult>UpdateProducts( int id,[FromForm]CreatePrdctDto prdctDto,IFormFile image)
        {
            var result=await _productInterface.UpdateProduct(id, prdctDto,image);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetByProductId/{id}")]
        public async Task <IActionResult> GetById(int id)
        {
            var result=await _productInterface.GetProductById(id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetByCategoryName/{name}")]
        
        public async Task<IActionResult> GetByName(string name)
        {
            var result=await _productInterface.GetProductByName(name);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/id")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result= await _productInterface.DeleteProduct(id);
            return Ok(result);
        }
    }
}

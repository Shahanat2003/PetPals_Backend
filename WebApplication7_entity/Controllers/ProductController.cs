using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using System.Net;
using WebApplication7_petPals.ApiStatusCode;
using WebApplication7_petPals.Models.Dto.ProductDto;
using WebApplication7_petPals.Models.Dto.UserDto;
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
        
        [HttpGet("AllProduct")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _productInterface.GetAllProducts();
                return Ok(response);

            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
           
        }


        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> PostPoduct([FromForm]CreatePrdctDto prdctDto,IFormFile image)
        {
            try
            {
                var response = await _productInterface.CreateProduct(prdctDto, image);
               
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }
           
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult>UpdateProducts( int id,[FromForm]CreatePrdctDto prdctDto,IFormFile image)
        {
            try
            {
                var response = await _productInterface.UpdateProduct(id, prdctDto, image);
                return Ok(response);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }
           
        }
        
        [HttpGet("GetByProductId")]
        public async Task <IActionResult> GetById(int id)
        {
            try
            {
                var respose = await _productInterface.GetProductById(id);
                return Ok(respose);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }
            
        }
       
        [HttpGet("GetByCategoryName")]
        
        public async Task<IActionResult> GetByName(string Catogory_name)
        {
            try
            {
                var response = await _productInterface.GetProductByName(Catogory_name);
                return Ok(response);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }
           
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result= await _productInterface.DeleteProduct(id);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchItems(string search)
        {
            var result=await _productInterface.Searchproduct(search);
            return Ok(result);

        }
        [HttpGet("pagination")]
        public async Task<IActionResult> Pagination(int pageNo,int size)
        {
            var result = await _productInterface.Pagination(pageNo, size);
            return Ok(result);

        }
    }
}

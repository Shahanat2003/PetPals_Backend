using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication7_petPals.ApiStatusCode;
using WebApplication7_petPals.Models.Dto.CatogoryDto;
using WebApplication7_petPals.Services.Catogory;

namespace WebApplication7_petPals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICatogryInterface _catogryInterface;
        public CategoryController(ICatogryInterface catogryInterface)
        {
            _catogryInterface = catogryInterface;
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        
        public async Task<IActionResult> PostCategory(CreateCatogoryDto createDto)
        {
            try
            {
                var response = await _catogryInterface.CreateCatogory(createDto);
                var result = new ApiResponse<bool>(HttpStatusCode.OK, true, "category Aded", response);
                return Ok(result);
            }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
            }
            
            

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("id")]
        public async Task<IActionResult> UpdateCategory(int id,CreateCatogoryDto catogoryDto)
        {
            try
            {
                var response = await _catogryInterface.UpdateCatogory(id, catogoryDto);
                var result = new ApiResponse<bool>(HttpStatusCode.OK, true, "categoryupdated", response);
                return Ok(result);

            }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var response = await _catogryInterface.DeleteCatogory(id);
                var result = new ApiResponse<bool>(HttpStatusCode.OK, true, "categoryupdated", response);
                return Ok(result);

            }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
            }
           
        }
       
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var result = await _catogryInterface.GetCategories();

                return Ok(result);

            }
            catch (Exception ex) { 
            return StatusCode(400,ex.Message);  
            }
            
        }

    }
}

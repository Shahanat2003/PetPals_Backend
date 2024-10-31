using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        
        public async Task<IActionResult> postCategory(CreateCatogoryDto createDto)
        {
            var result=await _catogryInterface.CreateCatogory(createDto);
            return Ok(result);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("id")]
        public async Task<IActionResult> UpdateCategory(int id,CreateCatogoryDto catogoryDto)
        {
            var result=await _catogryInterface.UpdateCatogory(id,catogoryDto);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result=await _catogryInterface.DeleteCatogory(id);
            return Ok(result);
        }
       
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result=await _catogryInterface.GetCategories();
            return Ok(result);
        }

    }
}

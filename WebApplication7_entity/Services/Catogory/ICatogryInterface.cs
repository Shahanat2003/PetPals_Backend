using WebApplication7_petPals.Models;
using WebApplication7_petPals.Models.Dto.CatogoryDto;

namespace WebApplication7_petPals.Services.Catogory
{
    public interface ICatogryInterface
    {
        Task <bool> CreateCatogory(CreateCatogoryDto createCatogoryDto);
        Task<bool> UpdateCatogory(int id,CreateCatogoryDto updateCatogoryDto);
        Task<bool> DeleteCatogory(int id);
        Task<List<OutCatogryDto>> GetCategories();

    }
}

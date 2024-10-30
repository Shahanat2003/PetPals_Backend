using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication7_petPals.Models;
using WebApplication7_petPals.Models.Dto.CatogoryDto;

namespace WebApplication7_petPals.Services.Catogory
{
    public class CatogoryServices : ICatogryInterface
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public CatogoryServices(IMapper mapper, AppDbContext appDbContext)
        {
            _context = appDbContext;
            _mapper = mapper;
        }
        public async Task<bool> CreateCatogory(CreateCatogoryDto CatogryDto)
        {
            try
            {
                var ctgry = _mapper.Map<Category>(CatogryDto);
                await _context.catogories.AddAsync(ctgry);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> UpdateCatogory(int id, CreateCatogoryDto catogoryDto)
        {
            try
            {
                var updateCatgry = await _context.catogories.FirstOrDefaultAsync(c => c.Id == id);
                if (updateCatgry != null)
                {
                    updateCatgry.Name = catogoryDto.Name;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public async Task<bool> DeleteCatogory(int id)
        {
            try
            {
                var delete = await _context.catogories.FirstOrDefaultAsync(c => c.Id == id);
                if (delete != null)
                {
                    _context.catogories.Remove(delete);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<List<OutCatogryDto>> GetCategories()
        {
            var allcategory=await _context.catogories.ToListAsync();
            return _mapper.Map<List<OutCatogryDto>>(allcategory);
        }
    }
}

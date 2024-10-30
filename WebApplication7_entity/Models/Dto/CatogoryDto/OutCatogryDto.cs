using System.ComponentModel.DataAnnotations;

namespace WebApplication7_petPals.Models.Dto.CatogoryDto
{
    public class OutCatogryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

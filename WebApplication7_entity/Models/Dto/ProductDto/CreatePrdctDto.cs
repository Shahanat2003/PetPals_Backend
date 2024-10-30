namespace WebApplication7_petPals.Models.Dto.ProductDto
{
    public class CreatePrdctDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OldPrice { get; set; }
        public int NewPrice { get; set; }
        public int CategoryId { get; set; }

    }
}

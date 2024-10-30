namespace WebApplication7_petPals.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Img { get; set; }
        public int OldPrice { get; set; }
        public int NewPrice { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<CartItem> CartItems { get; set; }
       
   
    }
}

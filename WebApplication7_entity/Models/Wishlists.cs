namespace WebApplication7_petPals.Models
{
    public class Wishlists
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public int Product_id { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}

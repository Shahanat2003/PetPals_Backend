namespace WebApplication7_petPals.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public virtual User User { get; set; }

        public virtual List<CartItem> CartItems { get; set; }

    }
}

namespace WebApplication7_petPals.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Cart_id { get; set; }
        public int Product_id { get; set; }
        public int Quantity { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }

    }
}

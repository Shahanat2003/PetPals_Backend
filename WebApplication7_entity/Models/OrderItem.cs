namespace WebApplication7_petPals.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Order_id { get; set; }
        public int Product_id { get; set; }
        public decimal Total_Price { get; set; }
        public int quantity { get; set; }



        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

    }
}

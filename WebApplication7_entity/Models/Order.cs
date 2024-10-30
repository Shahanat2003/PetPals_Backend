namespace WebApplication7_petPals.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Customer_Name { get; set; }
        public int User_id { get; set; }
        public DateTime Order_date { get; set; }
        public string Customer_email { get; set; }
        public string Customer_phone { get; set; }
        public string Customer_address { get; set; }
        public string Customer_city { get; set; }
        public string Order_string { get; set; }
        public int Transaction_id { get; set; }
        public string Order_id { get; set; }

        public virtual User User { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }

 
    }
}

namespace WebApplication7_petPals.Models.Dto.OrderDto
{
    public class OrderViewDto
    {
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public int quantity { get; set; }
        public decimal totalPrice  { get; set; }
        public DateTime orderDate { get; set; }
        public string image {  get; set; }
        public string order_id { get; set; }
        public string order_status { get; set; }
    }
}

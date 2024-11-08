namespace WebApplication7_petPals.Models.Dto.OrderDto
{
    public class OrderAdminDto
    {
        public int Customer_Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddres { get; set; }
        public string OrderId { get; set; }
        //public string Product_name { get; set; }
        //public string Product_image { get; set; }
      
    
        public DateTime Order_Date { get; set; }    
        public string TransactionId { get; set; }

        public List<OrderViewDto> Orders { get; set; }
    }
}

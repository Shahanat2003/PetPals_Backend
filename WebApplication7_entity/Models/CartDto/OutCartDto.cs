using System.ComponentModel;

namespace WebApplication7_petPals.Models.CartDto
{
    public class OutCartDto
    {
       
        public int Id { get; set; }
        [DisplayName("ProductName")]
        public string ProductName { get; set; }
        [DisplayName("Price")]
        public int Price { get; set; }
        [DisplayName("ImageURL")]
        public string Image { get; set; }
        [DisplayName("ToatalPrice")]
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }

    }
}

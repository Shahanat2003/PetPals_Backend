﻿namespace WebApplication7_petPals.Models.Dto.OrderDto
{
    public class OrderRequestDto
    {
        public string Customer_name { get; set; }

        public string Customer_email { get; set; }
        public string Customer_phone { get; set; }
        public string Customer_address { get; set; }
        public string Order_status { get; set; }
        public string Order_string { get; set; }
        public int Transaction_id { get; set; }
        public string Customer_city { get; set; }

    }
}
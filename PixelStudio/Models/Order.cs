using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PixelStudio.Models
{
    public class Order
    {
        public Order()
        {
            OrderID = 0;

        }

        public Order(int orderId, int userId)
        {
            this.OrderID = orderId;
            this.UserId = userId;
        }

        
        public int OrderID { get; set; }
        public int UserId { get; set; }
        public int TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
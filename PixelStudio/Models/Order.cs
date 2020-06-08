using PixelStudio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PixelStudio.Models
{
    public class Order
    {
        

        //public Order(int orderId, int userId)
        //{
        //    this.OrderID = orderId;
        //    this.UserId = userId;
        //}

        
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int ServiceID { get; set; }
        public string ServiceDesc { get; set; }
        public int NumbCopies { get; set; }
        public int TotalPrice { get; set; }
        public int StatusID { get; set; }
        public string StatusDesc { get; set; }
        public string UserInfo { get; set; }
        public string Image { get; set; }
        //public HomeSet homeSet { get; set; }


    }
}
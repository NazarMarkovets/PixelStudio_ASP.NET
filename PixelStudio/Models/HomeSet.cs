using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PixelStudio.ViewModels
{
    public class HomeSet
    {

        /// <summary>
        /// ДЛя Заказа
        /// </summary>
        
        
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string PhotoFormat { get; set; }
        public string Description { get; set; }
        public string ColorType { get; set; }
        public decimal Price { get; set; }
        
    }
}
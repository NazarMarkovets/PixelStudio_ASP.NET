using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PixelStudio.Models
{
    public class PhotoService
    {

        //public PhotoService()
        //{
        //    ServiceId = 0;
        //    Name = "";
        //}
        
        public int ServiceId { get; set; }
        
        //[Display(Name = "Service name :")]
        //[StringLength(maximumLength:100,MinimumLength = 5,ErrorMessage ="Service  from 5 to 100 symbols")]
        public string Name { get; set; }
        
        //[Display(Name = "Photo format (PNG,JPG...) :")]
        //[StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessage = "Format from 2 to 200 symbols")]
        public string PhotoFormat { get; set; }
        //[Display(Name = "Description :")]
        public string Description { get; set; }
        //[Display(Name = "Color type :")]
        public string ColorType { get; set; }
        //[Display(Name = "Price :")]
        public decimal Price { get; set; }

        //[Required(ErrorMessage = "Upload Image")]
        //[Display(Name = "Service Image :")]
        public string Image { get; set; }
    }
}
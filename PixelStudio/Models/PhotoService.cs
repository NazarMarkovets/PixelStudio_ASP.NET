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
        [Required(ErrorMessage = "Enter service name")]
        //[Display(Name = "Service name :")]
        //[StringLength(maximumLength:100,MinimumLength = 5,ErrorMessage ="Service  from 5 to 100 symbols")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Format is empty")]
        //[Display(Name = "Photo format (PNG,JPG...) :")]
        //[StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessage = "Format from 2 to 200 symbols")]
        public string PhotoFormat { get; set; }
        //[Display(Name = "Description :")]
        [Required(ErrorMessage = "Description is empty")]
        public string Description { get; set; }
        //[Display(Name = "Color type :")]
        [Required(ErrorMessage = "Color is empty")]
        public string ColorType { get; set; }
        //[Display(Name = "Price :")]
        [Required(ErrorMessage = "Price is empty")]
        public decimal Price { get; set; }

        //[Required(ErrorMessage = "Upload Image")]
        //[Display(Name = "Service Image :")]
        public string Image { get; set; }
    }
}
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
        //[Display(Name = "ServiceId :")]
        public int ServiceId { get; set; }
        //[Required(ErrorMessage = "Please enter correct service name")]
        [Display(Name = "Service name :")]
        [StringLength(maximumLength:100,MinimumLength = 5,ErrorMessage ="Service  from 5 to 100 symbols")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Please enter correct photo format")]
        [Display(Name = "Photo format (PNG,JPG...) :")]
        [StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessage = "Format from 2 to 200 symbols")]
        public string PhotoFormat { get; set; }
        //[Required(ErrorMessage = "Please enter descriptipn")]
        [Display(Name = "Description :")]
        //[StringLength(maximumLength: 200, MinimumLength = 5, ErrorMessage = "Format from 5 to 200 symbols")]
        public string Description { get; set; }
        //[Required(ErrorMessage = "Unchanged color type")]
        [Display(Name = "Color type :")]
        //[StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "Unchanged color type")]
        public string ColorType { get; set; }
        //[Required(ErrorMessage = "Please enter correct price")]
        [Display(Name = "Price :")]
        //[StringLength(maximumLength: 10, MinimumLength = 1, ErrorMessage = "Price must be more 1 symbol")]
        public decimal Price { get; set; }

        //[Required(ErrorMessage = "Upload Image")]
        //[Display(Name = "Service Image :")]
        public string Image { get; set; }
    }
}
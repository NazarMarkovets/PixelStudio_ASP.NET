using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PixelStudio.Models
{
    public class LoginSystem
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your Email")]
        [Display(Name = "Enter Email :")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your Password")]
        [Display(Name = "Enter Password :")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Users users { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PixelStudio.Models
{
    public class RegisterSystem
    {
        
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        [Required(ErrorMessage = "Please enter correct Phone")]
        [Display(Name = "Enter correct phone :")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Role { get; set; }
        [Required(ErrorMessage = "Please enter correct Email")]
        [Display(Name = "Enter correct email :")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
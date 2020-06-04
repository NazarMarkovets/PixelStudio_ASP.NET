using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PixelStudio.Models
{
    public class RegisterSystem
    {
        public string UserId { get; set; }

        [Display(Name = "Enter Name :")]
        public string UserName { get; set; }
        [Display(Name = "Enter Surname :")]
        public string UserSurname { get; set; }
        //[Required(ErrorMessage = "Please enter correct Phone")]
        [Display(Name = "Enter phone :")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Role { get; set; }
        [Required(ErrorMessage = "Please enter correct Email")]
        [Display(Name = "Enter email :")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password )]
        public string Password { get; set; }
    }
}
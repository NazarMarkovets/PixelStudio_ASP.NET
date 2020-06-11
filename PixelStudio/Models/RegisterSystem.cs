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
        [Required(ErrorMessage = "Name is empty")]
        public string UserName { get; set; }
        [Display(Name = "Enter Surname :")]
        [Required(ErrorMessage = "Surname is empty")]
        public string UserSurname { get; set; }
        //[Required(ErrorMessage = "Please enter correct Phone")]

        [Display(Name = "Enter phone :")]
        [Required(ErrorMessage = "Phone is empty")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(13, ErrorMessage = "Phone cannot be longer than 13 sumbols.")]
        public string Phone { get; set; }

        public string Role { get; set; }
        [Required(ErrorMessage = "Email is bad")]
        [Display(Name = "Enter email :")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "Email cannot be longer than 50 sumbols.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "No password we can see")]
        [DataType(DataType.Password )]
        public string Password { get; set; }
    }
}
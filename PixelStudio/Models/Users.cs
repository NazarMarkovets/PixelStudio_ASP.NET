using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PixelStudio.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
using PixelStudio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PixelStudio.ViewModels
{
    public class HomeSet
    {

        /// <summary>
        /// ДЛя Заказа
        /// </summary>

        //public int UserId { get; set; }
        public int StatusId { get; set; }
        public int serviceID { get; set; }
        
        [Display(Name = "Name")]
        public string UserName { get; set; }
        [Display(Name = "Surname :")]
        public string UserSurname { get; set; }
        [Display(Name = "Phone :")]
        public string Phone { get; set; }
        [Display(Name = "Enter Email :")]
        public string Email { get; set; }
        public string image { get; set; }
        public decimal Price { get; set; }
        public int code { get; set; }

        public int copies { get; set; }
        public int? UserId { get; set; }
        public RegisterSystem register { get; set; }

    }
}
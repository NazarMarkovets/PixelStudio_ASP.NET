using PixelStudio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelStudio.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult About_Company()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(HomeSet set)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanVePhim.Models;

namespace QLBanVePhim.Controllers
{
    public class HomeController : Controller
    {

        QLBanVePhimEntities database = new QLBanVePhimEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(khach_hang khachHang)
        {
            try
            {
                database.khach_hang.Add(khachHang);
                khachHang.ngay_dang_ky = DateTime.Today;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return Content(e.ToString());
            }
        }

        public ActionResult MovieList()
        {
            return View(database.phim.ToList());
        }

        
        public ActionResult Details(string id)
        {
            return View(database.phim.Where(s => s.id == id).FirstOrDefault());
        }

        public ActionResult Login()
        {
            return View();
        }

       

    }
}
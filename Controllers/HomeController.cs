using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (Session["UserID"] != null)
            {
                return View();
            }
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

        public ActionResult Login(khach_hang khachHang)
        {
            var currentUser = database.khach_hang.Where(user => user.email == khachHang.email && user.mat_khau == khachHang.mat_khau).FirstOrDefault();
            if (currentUser != null)
            {
                Debug.WriteLine(currentUser.ho_ten.ToString());
                Session["Username"] = currentUser.ho_ten.ToString();
                Session["Id"] = currentUser.id;
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }

        public ActionResult ThanhVien()
        {
            return View();
        }
       

    }
}
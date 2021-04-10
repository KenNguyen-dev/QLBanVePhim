using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanVePhim.Models;


namespace QLBanVePhim.Controllers
{
    
    public class QLHomeController : Controller
    {
        QLBanVePhimEntities db = new QLBanVePhimEntities();
        // GET: QLHome
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QLKH()
        {
            
            return View(db.khach_hang.ToList());
        }

        public ActionResult AddNV()
        {
            return View();
        }

        public ActionResult QLNV()
        {
            return View();
        }
    }
}
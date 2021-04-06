using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanVePhim.Controllers
{
    public class QLHomeController : Controller
    {
        // GET: QLHome
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QLKH()
        {
            return View();
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
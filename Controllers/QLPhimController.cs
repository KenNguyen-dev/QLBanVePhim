using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanVePhim.Controllers
{
    public class QLPhimController : Controller
    {
        // GET: QLPhim
        public ActionResult QLPhim()
        {
            return View();
        }

        public ActionResult AddPhim()
        {
            return View();
        }
    }
}
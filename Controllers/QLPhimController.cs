using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanVePhim.Models;

namespace QLBanVePhim.Controllers
{
    public class QLPhimController : Controller
    {
        QLBanVePhimEntities db = new QLBanVePhimEntities();
        // GET: QLPhim
        public ActionResult QLPhim()
        {
            return View(db.phims.ToList());
        }

        public ActionResult AddPhim()
        {
            return View();
        }

        public ActionResult EditPhim()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddPhim(phim _phim)
        {
            try
            {
                db.phims.Add(_phim);
                db.SaveChanges();
                return RedirectToAction("QLPhim");
            }
            catch (DbEntityValidationException e)
            {
                string error = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    error += ("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error += ("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return Content(error);
                throw;
            }
        }
    }
}
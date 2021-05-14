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
        private QLBanVePhimEntities db = new QLBanVePhimEntities();
        // GET: QLPhim

        public ActionResult QLPhim()
        {
            return View(db.phim.ToList());
        }

        public ActionResult EditPhim(string id)
        {
            var loaiphimList = db.loai_phim.ToList();
            ViewBag.LoaiPhimList = new SelectList(loaiphimList, "id", "ten");
            return View(db.phim.Where(s => s.id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditPhim(string id, phim phim)
        {
            db.Entry(phim).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("QLPhim");
        }

        [HttpPost]
        public ActionResult DeletePhim(string id, phim phim)
        {
            try
            {
                phim = db.phim.Where(item => item.id == id).FirstOrDefault();
                db.phim.Remove(phim);
                db.SaveChanges();
                return RedirectToAction("QLPhim");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        public ActionResult AddPhim()
        {
            var loaiphimList = db.loai_phim.ToList();
            ViewBag.LoaiPhimList = new SelectList(loaiphimList, "id", "ten");
            return View();
        }

        [HttpPost]
        public ActionResult AddPhim(phim _phim)
        {
            try
            {
                _phim.da_xoa = false;
                db.phim.Add(_phim);
                db.SaveChanges();
                return RedirectToAction("QLPhim");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }
    }
}
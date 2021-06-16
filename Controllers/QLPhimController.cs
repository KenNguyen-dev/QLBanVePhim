using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using QLBanVePhim.Models;

namespace QLBanVePhim.Controllers
{
    public class QLPhimController : Controller
    {
        private QLBanVePhimEntities db = new QLBanVePhimEntities();
        private int pageSize = 6;

        public bool AuthCheck(string perm)
        {
            bool check = true;

            if (Session["Id_Admin"] == null)
                return check = false;

            if (!String.IsNullOrEmpty(perm))
            {
                if (!Session["Id_VT_Admin"].Equals(perm))
                    check = false;
                if (Session["Id_VT_Admin"].Equals("admin"))
                    check = true;
            }
            return check;
        }

        // GET: QLPhim

        public ActionResult QLPhim(string tenPhim, string trangthaiPhim, int? page)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            var _phim = db.phim.ToList();
            if (!String.IsNullOrEmpty(tenPhim))
                _phim = _phim.Where(s => s.ten.ToLower().Contains(tenPhim.ToLower())).ToList();
            if (!String.IsNullOrEmpty(trangthaiPhim))
            {
                _phim = _phim.Where(s => s.trang_thai == trangthaiPhim).ToList();
            }
            ViewBag.CurrTen = tenPhim;
            ViewBag.CurrTT = trangthaiPhim;
            ViewBag.TrangThaiList = new SelectList(new List<SelectListItem>{
                     new SelectListItem { Selected = false, Text = "Chưa Chiếu", Value = "Chưa Chiếu"},
                     new SelectListItem { Selected = false, Text = "Đang Chiếu", Value = "Đang Chiếu"},
                     new SelectListItem { Selected = false, Text = "Ngưng Chiếu", Value = "Ngưng Chiếu"},
                }, "Value", "Text");
            int pageNum = (page ?? 1);
            return View(_phim.ToPagedList(pageNum, pageSize));
        }

        public ActionResult EditPhim(string id)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            var loaiphimList = db.loai_phim.ToList();
            ViewBag.LoaiPhimList = new SelectList(loaiphimList, "id", "ten");
            ViewBag.TrangThaiList = new SelectList(new List<SelectListItem>{
                     new SelectListItem { Selected = false, Text = "Chưa Chiếu", Value = "Chưa Chiếu"},
                     new SelectListItem { Selected = false, Text = "Đang Chiếu", Value = "Đang Chiếu"},
                     new SelectListItem { Selected = false, Text = "Ngưng Chiếu", Value = "Ngưng Chiếu"},
                }, "Value", "Text");
            phim _phim = new phim();
            _phim = db.phim.Where(s => s.id.Equals(id)).FirstOrDefault();
            return View(_phim);
        }

        [HttpPost]
        public ActionResult EditPhim(string id, phim phim)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            db.Entry(phim).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("QLPhim");
        }

        [HttpPost]
        public ActionResult DeletePhim(string id, phim phim)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
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
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            var loaiphimList = db.loai_phim.ToList();
            ViewBag.TrangThaiList = new SelectList(new List<SelectListItem>{
                     new SelectListItem { Selected = false, Text = "Chưa Chiếu", Value = "Chưa Chiếu"},
                     new SelectListItem { Selected = false, Text = "Đang Chiếu", Value = "Đang Chiếu"},
                     new SelectListItem { Selected = false, Text = "Ngưng Chiếu", Value = "Ngưng Chiếu"},
                }, "Value", "Text");
            ViewBag.LoaiPhimList = new SelectList(loaiphimList, "id", "ten");
            return View();
        }

        [HttpPost]
        public ActionResult AddPhim(phim _phim)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
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
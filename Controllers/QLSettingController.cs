using QLBanVePhim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanVePhim.Controllers
{
    public class QLSettingController : Controller
    {
        private QLBanVePhimEntities db = new QLBanVePhimEntities();

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

        // GET: QLSetting
        public ActionResult Index()
        {
            QuanLyClass.InitDataCheck();
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");

            return View();
        }

        #region Định Dạng Phim

        public ActionResult Render_DDP(string searchString)
        {
            var ddp = from m in db.dinh_dang_phim
                      select m;
            if (!string.IsNullOrEmpty(searchString))
            {
                ddp = ddp.Where(s => s.ten.Contains(searchString));
            }

            if (Request.IsAjaxRequest())
                return PartialView("_DinhDangPhim", ddp.ToList());

            return PartialView("_DinhDangPhim", db.dinh_dang_phim.ToList());
        }

        [HttpPost]
        public ActionResult Update_DDP(FormCollection form)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            string id = form["idDDP_edit"].ToString();
            dinh_dang_phim ddp = db.dinh_dang_phim.Where(item => item.id == id).FirstOrDefault();
            ddp.phu_thu = int.Parse(form["phuthuDDP_edit"]);
            ddp.ten = form["tenDDP_edit"];
            db.SaveChanges();
            return Redirect(Url.Action("Index", "QLSetting") + "#DDP");
        }

        #endregion Định Dạng Phim

        #region Loại Phim

        public ActionResult Render_LP(string searchString)
        {
            var lp = from m in db.loai_phim
                     select m;
            if (!string.IsNullOrEmpty(searchString))
            {
                lp = lp.Where(s => s.ten.Contains(searchString));
            }

            if (Request.IsAjaxRequest())
                return PartialView("_LoaiPhim", lp.ToList());
            return PartialView("_LoaiPhim", db.loai_phim.ToList());
        }

        [HttpPost]
        public ActionResult Add_LP(FormCollection form)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            try
            {
                loai_phim lp = new loai_phim();
                lp.id = form["idLP"];
                lp.ten = form["tenLP"];
                db.loai_phim.Add(lp);
                db.SaveChanges();
                return Redirect(Url.Action("Index", "QLSetting") + "#LP");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return View("~/Views/QLHome/Error.cshtml");
            }
        }

        [HttpPost]
        public ActionResult Update_LP(FormCollection form)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            string id = form["idLP_edit"].ToString();
            loai_phim lp = db.loai_phim.Where(item => item.id == id).FirstOrDefault();
            lp.ten = form["tenLP_edit"];
            db.SaveChanges();
            return Redirect(Url.Action("Index", "QLSetting") + "#LP");
        }

        [HttpPost]
        public ActionResult Delete_LP(FormCollection form)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            try
            {
                string id = form["idLP_delete"].ToString();
                loai_phim lp = db.loai_phim.Where(item => item.id == id).FirstOrDefault();
                db.Entry(lp).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return Redirect(Url.Action("Index", "QLSetting") + "#LP");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return View("~/Views/QLHome/Error.cshtml");
            }
        }

        #endregion Loại Phim

        #region Loại Ghế

        public ActionResult Render_LG()
        {
            return PartialView("_LoaiGhe", db.loai_ghe.ToList());
        }

        [HttpPost]
        public ActionResult Update_LG(FormCollection form)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            string id = form["idLG_edit"].ToString();
            loai_ghe lg = db.loai_ghe.Where(item => item.id == id).FirstOrDefault();
            lg.ten_ghe = form["tenLG_edit"];
            lg.phu_thu = int.Parse(form["phuthuLG_edit"]);
            db.SaveChanges();
            return Redirect(Url.Action("Index", "QLSetting") + "#LG");
        }

        #endregion Loại Ghế

        #region Giá Vé

        public ActionResult Render_GV()
        {
            return PartialView("_GiaVe", db.gia_ve.ToList());
        }

        [HttpPost]
        public ActionResult Update_GV(FormCollection form)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            string id = form["idGV_edit"].ToString();
            gia_ve gv = db.gia_ve.Where(item => item.id == id).FirstOrDefault();
            gv.ten = form["tenGV_edit"].ToString();
            gv.don_gia = int.Parse(form["dongiaGV_edit"]);
            db.SaveChanges();
            return Redirect(Url.Action("Index", "QLSetting") + "#GV");
        }

        #endregion Giá Vé

        #region Kích Cỡ Đồ Ăn

        public ActionResult Render_FS()
        {
            return PartialView("_FoodSize", db.kich_co_do_an.ToList());
        }

        [HttpPost]
        public ActionResult Update_FS(FormCollection form)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            string id = form["idFS_edit"].ToString();
            kich_co_do_an fs = db.kich_co_do_an.Where(item => item.id == id).FirstOrDefault();
            fs.ten = form["tenFS_edit"].ToString();
            db.SaveChanges();
            return Redirect(Url.Action("Index", "QLSetting") + "#FS");
        }

        #endregion Kích Cỡ Đồ Ăn

        #region Loại Đồ Ăn

        public ActionResult Render_LF(string searchString)
        {
            var lf = from m in db.loai_do_an
                     select m;
            if (!string.IsNullOrEmpty(searchString))
            {
                lf = lf.Where(s => s.ten.Contains(searchString));
            }

            if (Request.IsAjaxRequest())
                return PartialView("_LoaiFood", lf.ToList());
            return PartialView("_LoaiFood", db.loai_do_an.ToList());
        }

        [HttpPost]
        public ActionResult Add_LF(FormCollection form)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            try
            {
                loai_do_an lf = new loai_do_an();
                lf.id = form["idLF"];
                lf.ten = form["tenLF"];
                db.loai_do_an.Add(lf);
                db.SaveChanges();
                return Redirect(Url.Action("Index", "QLSetting") + "#LF");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return View("~/Views/QLHome/Error.cshtml");
            }
        }

        [HttpPost]
        public ActionResult Update_LF(FormCollection form)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            string id = form["idLF_edit"].ToString();
            loai_do_an lf = db.loai_do_an.Where(item => item.id == id).FirstOrDefault();
            lf.ten = form["tenLF_edit"];
            db.SaveChanges();
            return Redirect(Url.Action("Index", "QLSetting") + "#LF");
        }

        [HttpPost]
        public ActionResult Delete_LF(FormCollection form)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            try
            {
                string id = form["idLF_delete"].ToString();
                loai_do_an lf = db.loai_do_an.Where(item => item.id == id).FirstOrDefault();
                db.Entry(lf).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return Redirect(Url.Action("Index", "QLSetting") + "#LF");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return View("~/Views/QLHome/Error.cshtml");
            }
        }

        #endregion Loại Đồ Ăn
    }
}
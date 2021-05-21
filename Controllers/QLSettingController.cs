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

        private void InitDataCheck()
        {
            var list_GiaVe = db.gia_ve.ToList();
            var list_LoaiGhe = db.loai_ghe.ToList();

            if (list_GiaVe.Count() == 0) InitDataGenerator("GiaVe");
            if (list_LoaiGhe.Count() == 0) InitDataGenerator("LoaiGhe");
        }

        private void InitDataGenerator(string type)
        {
            switch (type)
            {
                case "GiaVe":
                    gia_ve gv = new gia_ve();
                    gv.id = "WEEKDAY";
                    gv.ten = "Ngay Thuong";
                    gv.don_gia = 50000;
                    db.gia_ve.Add(gv);

                    gia_ve gv2 = new gia_ve();
                    gv2.id = "WEEKEND";
                    gv2.ten = "Cuoi Tuan";
                    gv2.don_gia = 70000;
                    db.gia_ve.Add(gv2);

                    db.SaveChanges();
                    break;

                case "LoaiGhe":
                    loai_ghe lg = new loai_ghe();
                    lg.id = "NORMAL";
                    lg.ten_ghe = "Normal";
                    lg.phu_thu = 0;
                    db.loai_ghe.Add(lg);

                    loai_ghe lg2 = new loai_ghe();
                    lg2.id = "VIP";
                    lg2.ten_ghe = "VIP";
                    lg2.phu_thu = 0;
                    db.loai_ghe.Add(lg2);

                    db.SaveChanges();
                    break;

                default:
                    break;
            }
        }

        // GET: QLSetting
        public ActionResult Index()
        {
            InitDataCheck();
            return View();
        }

        #region Định Dạng Phim

        public ActionResult Render_DDP()
        {
            return PartialView("_DinhDangPhim", db.dinh_dang_phim.ToList());
        }

        [HttpPost]
        public ActionResult Add_DDP(FormCollection form)
        {
            try
            {
                dinh_dang_phim ddp = new dinh_dang_phim();
                ddp.id = form["idDDP"];
                ddp.ten = form["tenDDP"];
                ddp.phu_thu = int.Parse(form["phuthuDDP"]);
                db.dinh_dang_phim.Add(ddp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        [HttpPost]
        public ActionResult Update_DDP(FormCollection form)
        {
            string id = form["idDDP_edit"].ToString();
            dinh_dang_phim ddp = db.dinh_dang_phim.Where(item => item.id == id).FirstOrDefault();
            ddp.phu_thu = int.Parse(form["phuthuDDP_edit"]);
            ddp.ten = form["tenDDP_edit"];
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete_DDP(FormCollection form)
        {
            string id = form["idDDP_delete"].ToString();
            dinh_dang_phim ddp = db.dinh_dang_phim.Where(item => item.id == id).FirstOrDefault();
            db.Entry(ddp).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion Định Dạng Phim

        #region Loại Phim

        public ActionResult Render_LP()
        {
            return PartialView("_LoaiPhim", db.loai_phim.ToList());
        }

        [HttpPost]
        public ActionResult Add_LP(FormCollection form)
        {
            try
            {
                loai_phim lp = new loai_phim();
                lp.id = form["idLP"];
                lp.ten = form["tenLP"];
                db.loai_phim.Add(lp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        [HttpPost]
        public ActionResult Update_LP(FormCollection form)
        {
            string id = form["idLP_edit"].ToString();
            loai_phim lp = db.loai_phim.Where(item => item.id == id).FirstOrDefault();
            lp.ten = form["tenLP_edit"];
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete_LP(FormCollection form)
        {
            string id = form["idLP_delete"].ToString();
            loai_phim lp = db.loai_phim.Where(item => item.id == id).FirstOrDefault();
            db.Entry(lp).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
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
            string id = form["idLG_edit"].ToString();
            loai_ghe lg = db.loai_ghe.Where(item => item.id == id).FirstOrDefault();
            lg.ten_ghe = form["tenLG_edit"];
            lg.phu_thu = int.Parse(form["phuthuLG_edit"]);
            db.SaveChanges();
            return RedirectToAction("Index");
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
            string id = form["idGV_edit"].ToString();
            gia_ve gv = db.gia_ve.Where(item => item.id == id).FirstOrDefault();
            gv.ten = form["tenGV_edit"].ToString();
            gv.don_gia = int.Parse(form["dongiaGV_edit"]);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion Giá Vé
    }
}
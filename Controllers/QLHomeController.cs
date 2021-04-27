using QLBanVePhim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace QLBanVePhim.Controllers
{
    public class QLHomeController : Controller
    {
        private QLBanVePhimEntities db = new QLBanVePhimEntities();

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

        public ActionResult EditNV()
        {
            return View();
        }

        public ActionResult AddKH()
        {
            return View();
        }

        public ActionResult QLNV()
        {
            return View(db.nguoi_dung.ToList());
        }

        public ActionResult DetailsKH(int id)
        {
            return View(db.khach_hang.Where(s => s.id == id).FirstOrDefault());
        }

        public ActionResult QLSuatChieu()
        {
            return View(db.suat_chieu.ToList());
        }

        public ActionResult AddSuatChieu()
        {
            var phimList = db.phims.ToList();
            ViewBag.PhimList = new SelectList(phimList, "id", "ten");
            ViewBag.PhimImgList = new SelectList(phimList, "id", "hinh_anh");
            var ddphimList = db.dinh_dang_phim.ToList();
            ViewBag.DDPhimList = new SelectList(ddphimList, "id", "ten");
            var phongchieuList = db.phong_chieu.ToList();
            ViewBag.PhongChieuList = new SelectList(phongchieuList, "id", "id");
            return View();
        }

        [HttpPost]
        public ActionResult AddSuatChieu(suat_chieu _suatchieu)
        {
            try
            {
                db.suat_chieu.Add(_suatchieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        public ActionResult QLPhongChieu()
        {
            return View(db.phong_chieu.ToList());
        }

        public ActionResult AddPhongChieu()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPhongChieu(phong_chieu _phong)
        {
            try
            {
                _phong.so_luong_cot = 10;
                _phong.so_luong_day = 10;
                db.phong_chieu.Add(_phong);

                #region Add_ghế

                List<ghe_ngoi> ds_ghe = new List<ghe_ngoi>();
                string[] col = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
                for (int i = 1; i < 11; i++)
                {
                    for (int j = 1; j < 11; j++)
                    {
                        ghe_ngoi ghe = new ghe_ngoi();
                        ghe.da_chon = false;
                        ghe.phong_chieu_id = _phong.id;
                        ghe.vi_tri_day = i;
                        ghe.vi_tri_cot = col[j-1];
                        ghe.id = ghe.phong_chieu_id + "_" + ghe.vi_tri_day + ghe.vi_tri_cot;
                        ghe.loai_ghe_id = "NORMAL";
                        if ((i >= 2 && i <= 6) && (j >= 3 && j <= 6))
                        {
                            ghe.loai_ghe_id = "VIP";
                        }
                        ds_ghe.Add(ghe);
                    }
                }
                foreach (ghe_ngoi _ghe in ds_ghe)
                {
                    db.ghe_ngoi.Add(_ghe);
                }

                #endregion Add_ghế

                db.SaveChanges();
                return RedirectToAction("QLPhongChieu");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        public ActionResult EditPhongChieu()
        {
            return View();
        }

        public ActionResult QLVe()
        {
            return View();
        }

        public ActionResult QLDoAn()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
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

        public ActionResult DetailsKH(int id)
        {
            return View(db.khach_hang.Where(s => s.id == id).FirstOrDefault());
        }

        public ActionResult AddKH()
        {
            return View();
        }

        #region Nhân Viên

        public ActionResult QLNV()
        {
            return View(db.nguoi_dung.ToList());
        }

        public ActionResult AddNV()
        {
            var vaitrolist = db.vai_tro.ToList();
            ViewBag.VaiTroList = new SelectList(vaitrolist, "id", "ten");
            return View();
        }

        [HttpPost]
        public ActionResult AddNV(nguoi_dung _ngdung)
        {
            try
            {
                _ngdung.dang_lam = true;
                _ngdung.ngay_vao_lam = DateTime.Now;
                db.nguoi_dung.Add(_ngdung);
                db.SaveChanges();
                return RedirectToAction("QLNV");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        public ActionResult EditNV(int id)
        {
            var vaitrolist = db.vai_tro.ToList();
            ViewBag.VaiTroList = new SelectList(vaitrolist, "id", "ten");
            return View(db.nguoi_dung.Where(s => s.id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditNV(int id, nguoi_dung _ngdung)
        {
            db.Entry(_ngdung).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("QLNV");
        }

        [HttpPost]
        public ActionResult DeleteNV(int id, nguoi_dung _ngdung)
        {
            try
            {
                _ngdung = db.nguoi_dung.Where(item => item.id == id).FirstOrDefault();
                db.nguoi_dung.Remove(_ngdung);
                db.SaveChanges();
                return RedirectToAction("QLNV");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        #endregion Nhân Viên

        #region Suất chiếu

        public ActionResult QLSuatChieu()
        {
            return View(db.suat_chieu.ToList());
        }

        public ActionResult AddSuatChieu()
        {
            var phimList = db.phim.ToList();
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
                return RedirectToAction("QLSuatChieu");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        public ActionResult EditSuatChieu(string id)
        {
            var phimList = db.phim.ToList();
            ViewBag.PhimList = new SelectList(phimList, "id", "ten");
            ViewBag.PhimImgList = new SelectList(phimList, "id", "hinh_anh");
            var ddphimList = db.dinh_dang_phim.ToList();
            ViewBag.DDPhimList = new SelectList(ddphimList, "id", "ten");
            var phongchieuList = db.phong_chieu.ToList();
            ViewBag.PhongChieuList = new SelectList(phongchieuList, "id", "id");
            return View(db.suat_chieu.Where(s => s.id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditSuatChieu(string id, suat_chieu _suatchieu)
        {
            db.Entry(_suatchieu).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("QLSuatChieu");
        }

        [HttpPost]
        public ActionResult DeleteSuatChieu(string id, suat_chieu _suatchieu)
        {
            try
            {
                _suatchieu = db.suat_chieu.Where(item => item.id == id).FirstOrDefault();
                db.suat_chieu.Remove(_suatchieu);
                db.SaveChanges();
                return RedirectToAction("QLSuatChieu");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        #endregion Suất chiếu

        #region Phòng chiếu

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
                        ghe.vi_tri_cot = col[j - 1];
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

        public ActionResult EditPhongChieu(int id)
        {
            return View(db.phong_chieu.Where(s => s.id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult DeletePhongChieu(int id)
        {
            try
            {
                phong_chieu phong_Chieu = db.phong_chieu.Where(item => item.id == id).FirstOrDefault();
                List<ghe_ngoi> ghe_Ngoi = db.ghe_ngoi.Where(item => item.phong_chieu_id == id).ToList();
                foreach (ghe_ngoi ghe in ghe_Ngoi)
                {
                    db.ghe_ngoi.Remove(ghe);
                }
                db.phong_chieu.Remove(phong_Chieu);
                db.SaveChanges();
                return RedirectToAction("QLPhongChieu");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        #endregion Phòng chiếu

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
using QLBanVePhim.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace QLBanVePhim.Controllers
{
    public class QLHomeController : Controller
    {
        private QLBanVePhimEntities db = new QLBanVePhimEntities();

        public bool AuthCheck()
        {
            if (Session["Id_Admin"] == null)
                return false;
            else
                return true;
        }

        // GET: QLHome
        public ActionResult Index()
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
            return View();
        }

        public ActionResult QLKH()
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
            return View(db.khach_hang.ToList());
        }

        public ActionResult DetailsKH(int id)
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
            return View(db.khach_hang.Where(s => s.id == id).FirstOrDefault());
        }

        public ActionResult AddKH()
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
            return View();
        }

        #region Nhân Viên

        public ActionResult QLNV()
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
            return View(db.nguoi_dung.ToList());
        }

        public ActionResult AddNV()
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
            var vaitrolist = db.vai_tro.ToList();
            ViewBag.VaiTroList = new SelectList(vaitrolist, "id", "ten");
            return View();
        }

        [HttpPost]
        public ActionResult AddNV(nguoi_dung _ngdung)
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
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
            if (!AuthCheck())
                return RedirectToAction("Login");
            var vaitrolist = db.vai_tro.ToList();
            ViewBag.VaiTroList = new SelectList(vaitrolist, "id", "ten");
            return View(db.nguoi_dung.Where(s => s.id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditNV(int id, nguoi_dung _ngdung)
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
            db.Entry(_ngdung).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("QLNV");
        }

        [HttpPost]
        public ActionResult DeleteNV(int id, nguoi_dung _ngdung)
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
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
            if (!AuthCheck())
                return RedirectToAction("Login");
            return View(db.suat_chieu.ToList());
        }

        public ActionResult AddSuatChieu()
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
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
            if (!AuthCheck())
                return RedirectToAction("Login");
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
            if (!AuthCheck())
                return RedirectToAction("Login");
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
            if (!AuthCheck())
                return RedirectToAction("Login");
            db.Entry(_suatchieu).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("QLSuatChieu");
        }

        [HttpPost]
        public ActionResult DeleteSuatChieu(string id, suat_chieu _suatchieu)
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
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
            if (!AuthCheck())
                return RedirectToAction("Login");
            return View(db.phong_chieu.ToList());
        }

        public ActionResult AddPhongChieu()
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
            return View();
        }

        [HttpPost]
        public ActionResult AddPhongChieu(phong_chieu _phong)
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
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
            if (!AuthCheck())
                return RedirectToAction("Login");
            return View(db.phong_chieu.Where(s => s.id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult DeletePhongChieu(int id)
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
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
            if (!AuthCheck())
                return RedirectToAction("Login");
            return View();
        }

        public ActionResult QLDoAn()
        {
            if (!AuthCheck())
                return RedirectToAction("Login");
            return View();
        }

        #region Login

        private void InitDataCheck()
        {
            var list_VT = db.vai_tro.Where(x => x.id == "admin").ToList();
            if (list_VT.Count() == 0) InitDataGenerator("VT");

            var list_NV = db.nguoi_dung.Where(x => x.vai_tro.ten == "Admin").ToList();
            if (list_NV.Count() == 0) InitDataGenerator("NV");

            var list_VT2 = db.vai_tro.Where(x => x.id == "nhanvien").ToList();
            if (list_VT2.Count() == 0) InitDataGenerator("VT2");
        }

        private void InitDataGenerator(string type)
        {
            switch (type)
            {
                case "VT":
                    vai_tro vt = new vai_tro();
                    vt.id = "admin";
                    vt.ten = "Admin";
                    db.vai_tro.Add(vt);
                    db.SaveChanges();
                    break;

                case "NV":
                    nguoi_dung nd = new nguoi_dung();
                    nd.id = 0;
                    nd.email = "admin@website.com";
                    nd.ho_ten = "Admin";
                    nd.so_cmnd = "0";
                    nd.gioi_tinh = true;
                    nd.dang_lam = true;
                    nd.ngay_vao_lam = DateTime.Now;
                    nd.dia_chi = "";
                    nd.so_dien_thoai = "0";
                    nd.mat_khau = "admin@123";
                    nd.vai_tro_id = "admin";
                    db.nguoi_dung.Add(nd);
                    db.SaveChanges();
                    break;

                case "VT2":
                    vai_tro vt2 = new vai_tro();
                    vt2.id = "nhanvien";
                    vt2.ten = "Nhân Viên";
                    db.vai_tro.Add(vt2);
                    db.SaveChanges();
                    break;

                default:
                    break;
            }
        }

        public ActionResult Login()
        {
            InitDataCheck();
            return View();
        }

        public ActionResult LogOut()
        {
            if (Session["Id_Admin"] != null)
            {
                Session["Username_Admin"] = null;
                Session["Id_Admin"] = null;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Login(nguoi_dung _ngdung)
        {
            var currentUser = db.nguoi_dung.Where(user => user.email == _ngdung.email && user.mat_khau == _ngdung.mat_khau).FirstOrDefault();
            if (currentUser != null)
            {
                Session["Username_Admin"] = currentUser.ho_ten.ToString();
                Session["Id_Admin"] = currentUser.id.ToString();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");
        }

        #endregion Login
    }
}
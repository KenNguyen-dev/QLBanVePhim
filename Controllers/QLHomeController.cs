using QLBanVePhim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace QLBanVePhim.Controllers
{
    public static class QuanLyClass
    {
        public static void TicketCheck()
        {
            QLBanVePhimEntities db = new QLBanVePhimEntities();
            var dsve = db.ve_ban.Where(s => s.trang_thai == "Book").ToList();
            bool flag = false;
            foreach (ve_ban ve in dsve)
            {
                DateTime ngChieu = (DateTime)ve.suat_chieu.ngay_chieu;
                TimeSpan gioChieu = (TimeSpan)ve.suat_chieu.gio_bat_dau;
                if (ngChieu.Date < DateTime.Today)
                {
                    flag = true;
                }
                else if (ngChieu.Date == DateTime.Today)
                {
                    if (DateTime.Now.TimeOfDay >= gioChieu)
                    {
                        flag = true;
                    }
                }

                if (flag)
                {
                    ve.ghe_ngoi.da_chon = false;
                    ve.trang_thai = "Cancelled";
                }
            }
            db.SaveChanges();
        }
    }

    public class QLHomeController : Controller
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

        // GET: QLHome
        public ActionResult Index()
        {
            if (!AuthCheck(null))
                return RedirectToAction("Login");
            return View();
        }

        #region Khách Hàng

        public ActionResult QLKH(string tenKH, string sdtKH, string cmndKH, int? page)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
            else
            {
                var khs = from kh in db.khach_hang
                          select kh;
                if (!String.IsNullOrEmpty(tenKH))
                {
                    khs = khs.Where(nv => nv.ho_ten.ToLower().Contains(tenKH.ToLower()));
                }
                if (!String.IsNullOrEmpty(sdtKH))
                {
                    khs = khs.Where(nv => nv.so_dien_thoai.Contains(sdtKH));
                }
                if (!String.IsNullOrEmpty(cmndKH))
                {
                    khs = khs.Where(nv => nv.so_cmnd.Contains(cmndKH));
                }
                ViewBag.CurrTen = tenKH;
                ViewBag.CurrSDT = sdtKH;
                ViewBag.CurrCMND = cmndKH;
                var list = khs.ToList();
                int pageNum = (page ?? 1);
                return View(list.ToPagedList(pageNum, pageSize));
            }
        }

        public ActionResult DetailsKH(int id)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
            return View(db.khach_hang.Where(s => s.id == id).FirstOrDefault());
        }

        public ActionResult AddKH()
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
            return View();
        }

        #endregion Khách Hàng

        #region Nhân Viên

        public ActionResult QLNV(string searchName, string searchSDT, string searchCMND, int? page)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
            else
            {
                var nvs = from nv in db.nguoi_dung
                          select nv;
                if (!String.IsNullOrEmpty(searchName))
                {
                    nvs = nvs.Where(nv => nv.ho_ten.ToLower().Contains(searchName.ToLower()));
                }
                if (!String.IsNullOrEmpty(searchSDT))
                {
                    nvs = nvs.Where(nv => nv.so_dien_thoai.Contains(searchSDT));
                }
                if (!String.IsNullOrEmpty(searchCMND))
                {
                    nvs = nvs.Where(nv => nv.so_cmnd.Contains(searchCMND));
                }
                ViewBag.CurrTen = searchName;
                ViewBag.CurrSDT = searchSDT;
                ViewBag.CurrCMND = searchCMND;
                var list = nvs.ToList();
                int pageNum = (page ?? 1);
                return View(list.ToPagedList(pageNum, pageSize));
            }
        }

        public ActionResult AddNV()
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
            var vaitrolist = db.vai_tro.ToList();
            ViewBag.VaiTroList = new SelectList(vaitrolist, "id", "ten");
            return View();
        }

        [HttpPost]
        public ActionResult AddNV(nguoi_dung _ngdung)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
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
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
            var vaitrolist = db.vai_tro.ToList();
            ViewBag.VaiTroList = new SelectList(vaitrolist, "id", "ten");
            return View(db.nguoi_dung.Where(s => s.id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditNV(int id, nguoi_dung _ngdung)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
            db.Entry(_ngdung).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("QLNV");
        }

        [HttpPost]
        public ActionResult DeleteNV(int id, nguoi_dung _ngdung)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
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

        public ActionResult QLSuatChieu(string idSC, string tenPhimSC, string ddphimSC, string phongSC, int? page)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
            else
            {
                var scs = from sc in db.suat_chieu
                          select sc;
                if (!String.IsNullOrEmpty(idSC))
                {
                    scs = scs.Where(sc => sc.id.ToString().Contains(idSC));
                }
                if (!String.IsNullOrEmpty(tenPhimSC))
                {
                    scs = scs.Where(sc => sc.phim.ten.ToLower().Contains(tenPhimSC.ToLower()));
                }
                if (!String.IsNullOrEmpty(ddphimSC))
                {
                    scs = scs.Where(sc => sc.dinh_dang_phim.ten == ddphimSC);
                }
                if (!String.IsNullOrEmpty(phongSC))
                {
                    scs = scs.Where(sc => sc.phong_chieu_id.ToString().Contains(idSC));
                }
                var ddphimList = db.dinh_dang_phim.ToList();
                ViewBag.DDPhimList = new SelectList(ddphimList, "id", "ten");

                ViewBag.CurrId = idSC;
                ViewBag.CurrTenP = tenPhimSC;
                ViewBag.CurrDDP = ddphimSC;
                ViewBag.CurrPC = phongSC;
                var list = scs.ToList();
                int pageNum = (page ?? 1);
                return View(list.ToPagedList(pageNum, pageSize));
            }
        }

        public ActionResult AddSuatChieu()
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
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
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
            try
            {
                phim _phim = new phim();
                _phim = db.phim.Where(s => s.id == _suatchieu.phim_id).FirstOrDefault();
                TimeSpan time = (TimeSpan)_suatchieu.gio_bat_dau;
                TimeSpan thoiLuong = TimeSpan.FromMinutes(Convert.ToDouble(_phim.thoi_luong));
                _suatchieu.gio_ket_thuc = time.Add(thoiLuong);
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
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
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
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
            phim _phim = new phim();
            _phim = db.phim.Where(s => s.id == _suatchieu.phim_id).FirstOrDefault();
            TimeSpan time = (TimeSpan)_suatchieu.gio_bat_dau;
            TimeSpan thoiLuong = TimeSpan.FromMinutes(Convert.ToDouble(_phim.thoi_luong));
            _suatchieu.gio_ket_thuc = time.Add(thoiLuong);
            db.Entry(_suatchieu).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("QLSuatChieu");
        }

        [HttpPost]
        public ActionResult DeleteSuatChieu(string id, suat_chieu _suatchieu)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
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

        public ActionResult QLPhongChieu(string idPhong, int? page)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
            else
            {
                var phongs = from phong in db.phong_chieu
                             select phong;
                if (!String.IsNullOrEmpty(idPhong))
                {
                    phongs = phongs.Where(phong => phong.id.ToString().Contains(idPhong));
                }
                ViewBag.CurrId = idPhong;
                var list = phongs.ToList();
                int pageNum = (page ?? 1);
                return View(list.ToPagedList(pageNum, pageSize));
            }
        }

        public ActionResult AddPhongChieu()
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public ActionResult AddPhongChieu(phong_chieu _phong)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
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
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
            return View(db.phong_chieu.Where(s => s.id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult DeletePhongChieu(int id)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index");
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

        #region Vé

        public ActionResult QLVe(string idVe, string trangthaiVe, int? page)
        {
            QuanLyClass.TicketCheck();
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index");
            else
            {
                var ves = from ve in db.ve_ban
                          select ve;
                if (!String.IsNullOrEmpty(idVe))
                {
                    ves = ves.Where(ve => ve.id.ToString().Contains(idVe));
                }
                if (!String.IsNullOrEmpty(trangthaiVe))
                {
                    ves = ves.Where(ve => ve.trang_thai == trangthaiVe);
                }
                ViewBag.TrangThaiList = new SelectList(new List<SelectListItem>{
                     new SelectListItem { Selected = false, Text = "Book", Value = "Book"},
                     new SelectListItem { Selected = false, Text = "Sold", Value = "Sold"},
                     new SelectListItem { Selected = false, Text = "Cancelled", Value = "Cancelled"},
                }, "Value", "Text");
                ViewBag.CurrTT = trangthaiVe;
                ViewBag.CurrId = idVe;
                var listVe = ves.ToList();
                int pageNum = (page ?? 1);
                return View(listVe.ToPagedList(pageNum, pageSize));
            }
        }

        public ActionResult DetailsVe(string id)
        {
            QuanLyClass.TicketCheck();
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index");
            return View(db.ve_ban.Where(s => s.id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult SoldVe(string id)
        {
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index");
            ve_ban ve = db.ve_ban.Where(s => s.id == id).FirstOrDefault();
            ve.nhan_vien_id = Convert.ToInt32(Session["ID_Admin"]);
            ve.ngay_ban = DateTime.Now;
            ve.trang_thai = "Sold";
            ve.ghe_ngoi.da_chon = false;
            db.SaveChanges();
            return RedirectToAction("QLVe");
        }

        [HttpPost]
        public ActionResult CancelVe(string id)
        {
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index");
            ve_ban ve = db.ve_ban.Where(s => s.id == id).FirstOrDefault();
            ve.nhan_vien_id = Convert.ToInt32(Session["ID_Admin"]);
            ve.ngay_ban = DateTime.Now;
            ve.trang_thai = "Cancelled";
            ve.ghe_ngoi.da_chon = false;
            db.SaveChanges();
            return RedirectToAction("QLVe");
        }

        #endregion Vé

        #region Batch Vé

        public ActionResult QLBatchVe(string idVe, int? page)
        {
            QuanLyClass.TicketCheck();
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index");
            else
            {
                int pageNum = (page ?? 1);
                var ves = from ve in db.ve_dat
                          join vedat in db.ve_dat_chi_tiet on ve.id equals vedat.ve_dat_id
                          join veban in db.ve_ban on vedat.id equals veban.id
                          where veban.trang_thai == "Book"
                          select ve;
                if (!String.IsNullOrEmpty(idVe))
                {
                    ves = ves.Where(ve => ve.id.ToString().Contains(idVe));
                }
                var listVe = ves.ToList().Distinct();
                ViewBag.CurrId = idVe;
                return View(listVe.ToPagedList(pageNum, pageSize));
            }
        }

        public ActionResult DetailsBatchVe(string id)
        {
            QuanLyClass.TicketCheck();
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index");
            var ves = from ve in db.ve_dat
                      join vedat in db.ve_dat_chi_tiet on ve.id equals vedat.ve_dat_id
                      join veban in db.ve_ban on vedat.id equals veban.id
                      where ve.id == id && veban.trang_thai == "Book"
                      select veban;
            ViewBag.BatchId = id;
            return View(ves.ToList());
        }

        [HttpPost]
        public ActionResult SoldBatchVe(string id)
        {
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index");

            var ves = from vd in db.ve_dat
                      join vedat in db.ve_dat_chi_tiet on vd.id equals vedat.ve_dat_id
                      join veban in db.ve_ban on vedat.id equals veban.id
                      where vd.id == id && veban.trang_thai == "Book"
                      select veban;
            var listVe = ves.ToList();
            foreach (var ve in listVe)
            {
                ve.nhan_vien_id = Convert.ToInt32(Session["ID_Admin"]);
                ve.ngay_ban = DateTime.Now;
                ve.trang_thai = "Sold";
                ve.ghe_ngoi.da_chon = false;
            }
            db.SaveChanges();
            return RedirectToAction("QLVe");
        }

        [HttpPost]
        public ActionResult CancelBatchVe(string id)
        {
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index");
            var ves = from vd in db.ve_dat
                      join vedat in db.ve_dat_chi_tiet on vd.id equals vedat.ve_dat_id
                      join veban in db.ve_ban on vedat.id equals veban.id
                      where vd.id == id && veban.trang_thai == "Book"
                      select veban;
            var listVe = ves.ToList();
            foreach (var ve in listVe)
            {
                ve.nhan_vien_id = Convert.ToInt32(Session["ID_Admin"]);
                ve.ngay_ban = DateTime.Now;
                ve.trang_thai = "Cancelled";
                ve.ghe_ngoi.da_chon = false;
            }
            db.SaveChanges();
            return RedirectToAction("QLVe");
        }

        #endregion Batch Vé

        public ActionResult QLDoAn()
        {
            if (!AuthCheck(null))
                return RedirectToAction("Index");
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
                Session["Id_VT_Admin"] = null;
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Login(nguoi_dung _ngdung)
        {
            var currentUser = db.nguoi_dung.Where(user => user.email == _ngdung.email && user.mat_khau == _ngdung.mat_khau).FirstOrDefault();
            if (currentUser != null)
            {
                Session["Username_Admin"] = currentUser.ho_ten.ToString();
                Session["Id_Admin"] = currentUser.id.ToString();
                Session["Id_VT_Admin"] = currentUser.vai_tro_id.ToString();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");
        }

        #endregion Login
    }
}
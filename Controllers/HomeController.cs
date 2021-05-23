using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using QLBanVePhim.Models;

namespace QLBanVePhim.Controllers
{
    public class HomeController : Controller
    {
        private QLBanVePhimEntities database = new QLBanVePhimEntities();

        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ThanhVien()
        {
            if (Session["Id"] != null)
            {
                int id = Convert.ToInt32(Session["Id"]);
                return View(database.khach_hang.Where(kh => kh.id == id).FirstOrDefault());
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Register(khach_hang khachHang)
        {
            try
            {
                database.khach_hang.Add(khachHang);
                khachHang.ngay_dang_ky = DateTime.Today;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        public ActionResult MovieList()
        {
            return View(database.phim.ToList());
        }

        public ActionResult TicketPrice()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            return View(database.phim.Where(s => s.id == id).FirstOrDefault());
        }

        [HttpGet]
        public ActionResult OrderTicket(string id)
        {
            if (Session["Id"] != null || Session["Username"] != null)
            {
                var suatChieu = database.suat_chieu.ToList().Where(s => s.phim_id == id);
                ViewBag.SuatChieu = suatChieu;

                return View();
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult OrderTicket(string suatChieu, string dsGhe)
        {
            try
            {
                //Session["Id"] = 1;
                //Dòng trên chỉ để test code
                suat_chieu sc = database.suat_chieu.Where(s => s.id == suatChieu).FirstOrDefault();
                ve_dat veDat = new ve_dat();

                string[] listGhe = dsGhe.Split(',');
                int tienDinhDangPhim = 0;
                int tongTien = 0;

                if (sc.dinh_dang_phim_id != "2D")
                {
                    tienDinhDangPhim = (int)sc.dinh_dang_phim.phu_thu;
                }

                veDat.id = Session["Id"].ToString() + "-" + sc.id + "-" + DateTime.Now.Second;
                veDat.khach_hang_id = Convert.ToInt32(Session["Id"]);
                veDat.ngay_dat = DateTime.Now.Date;

                database.ve_dat.Add(veDat);
                database.SaveChanges();

                foreach (var item in listGhe)
                {
                    ve_ban veBan = new ve_ban();
                    ve_dat_chi_tiet veDatChiTiet = new ve_dat_chi_tiet();
                    ghe_ngoi ghe = database.ghe_ngoi.Where(g => g.id == item).FirstOrDefault();
                    ghe.da_chon = true;
                    veBan.id = sc.id + "-" + ghe.id;
                    veBan.suat_chieu_id = sc.id;
                    if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday || DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
                    {
                        veBan.gia_ve_id = "WEEKEND";
                        gia_ve giaVe = database.gia_ve.Where(gv => gv.id == veBan.gia_ve_id).FirstOrDefault();
                        veBan.tong__tien = tienDinhDangPhim + ghe.loai_ghe.phu_thu + giaVe.don_gia;
                        tongTien += (int)veBan.tong__tien;
                    }
                    else
                    {
                        veBan.gia_ve_id = "WEEKDAY";
                        gia_ve giaVe = database.gia_ve.Where(gv => gv.id == veBan.gia_ve_id).FirstOrDefault();
                        veBan.tong__tien = tienDinhDangPhim + ghe.loai_ghe.phu_thu + giaVe.don_gia;
                        tongTien += (int)veBan.tong__tien;
                    }
                    veBan.ghe_id = ghe.id;
                    veBan.trang_thai = "Book";
                    veBan.nhan_vien_id = 1;
                    //Dòng trên chỉ để test code

                    veDatChiTiet.id = veBan.id;
                    veDatChiTiet.ve_dat_id = veDat.id;

                    database.ve_ban.Add(veBan);
                    database.ve_dat_chi_tiet.Add(veDatChiTiet);
                    database.SaveChanges();
                }
                TempData["CodeDatVe"] = veDat.id.ToString();
                TempData["MaKhachHang"] = veDat.khach_hang_id.ToString();
                TempData["Phim"] = sc.phim.ten.ToString();
                TempData["ThoiLuong"] = sc.phim.thoi_luong.ToString();
                TempData["BatDau"] = sc.gio_bat_dau.ToString();
                TempData["TongTien"] = tongTien.ToString();

                return Redirect("Confirmation");
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        public ActionResult GetRoom(int id, string scId)
        {
            var veBan = database.ve_ban.Where(v => v.suat_chieu_id == scId).OrderBy(g=>g.ghe_id).ToList();
            List<string> dsGheDaChon = new List<string>();
            foreach (var ghe in veBan)
            {
                dsGheDaChon.Add(ghe.ghe_id);
            }
            var phongChieu = database.phong_chieu.Where(p => p.id == id).FirstOrDefault();
            var dsGhe = database.ghe_ngoi.Where(g => g.phong_chieu.id == phongChieu.id).OrderBy(s => s.vi_tri_day).ToList();
            var suatChieu = database.suat_chieu.Where(s => s.phong_chieu_id == id).ToList();

            ViewBag.RoomNumber = phongChieu.id;
            ViewBag.DsGhe = dsGhe;
            ViewBag.SuatChieu = suatChieu;
            ViewBag.SuatChieuChon = scId;
            ViewBag.GheDaChon = dsGheDaChon;

            return View("OrderTicket");
        }

        public ActionResult Login(khach_hang khachHang)
        {
            var currentUser = database.khach_hang.Where(user => user.email == khachHang.email && user.mat_khau == khachHang.mat_khau).FirstOrDefault();
            if (currentUser != null)
            {
                Session["Username"] = currentUser.ho_ten.ToString();
                Session["Id"] = currentUser.id.ToString();
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }

        public ActionResult LichChieuPhim(string id)
        {
            return View(database.suat_chieu.Where(s => s.phim_id == id).FirstOrDefault());
        }

        public ActionResult LichChieuPhimNgay(string id)
        {
            return View(database.suat_chieu.Where(s => s.phim_id == id).FirstOrDefault());
        }

        public ActionResult Confirmation()
        {
            return View();
        }

        public ActionResult TicketList()
        {
            QuanLyClass.TicketCheck();
            List<List<ve_ban>> dsVe = new List<List<ve_ban>>();
            var khach_hang_id = Convert.ToInt32(Session["Id"]);

            var veDat = database.ve_dat.Where(vd => vd.khach_hang_id == khach_hang_id).ToList();

            foreach (var vd in veDat)
            {
                dsVe.Add(database.ve_ban.Where(vb => vb.ve_dat_chi_tiet.ve_dat_id == vd.id && vb.trang_thai == "Book").ToList());
            }
            ViewBag.DsVe = dsVe;

            return View();
        }

        public ActionResult CancelTicket(string id)
        {
            return View(database.ve_ban.Where(s => s.id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult CancelTicket(string id, ve_ban veBan, ve_dat_chi_tiet vdct, ghe_ngoi ghe)
        {
            try
            {
                veBan = database.ve_ban.Where(s => s.id == id).FirstOrDefault();
                vdct = database.ve_dat_chi_tiet.Where(s => s.id == id).FirstOrDefault();
                ghe = database.ghe_ngoi.Where(g => g.id == veBan.ghe_id).FirstOrDefault();

                ghe.da_chon = false;
                veBan.trang_thai = "Cancelled";
                //database.ve_dat_chi_tiet.Remove(vdct);
                database.SaveChanges();
                return RedirectToAction("TicketList");
            }
            catch
            {
                return Content("Error Delete!");
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }

    }
}
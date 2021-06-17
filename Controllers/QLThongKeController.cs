using QLBanVePhim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanVePhim.Controllers
{
    public class QLThongKeController : Controller
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

        // GET: QLThongKe
        public ActionResult Index()
        {
            QuanLyClass.TicketCheck();
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLHome");
            return View();
        }

        public ActionResult Render_Overview()
        {
            ViewBag.FoodTotal = db.hoa_don_chi_tiet.Sum(x => x.tong_tien) ?? default(int);
            ViewBag.FoodNumber = db.hoa_don_chi_tiet.Sum(x => x.so_luong) ?? default(int);
            ViewBag.FoodCount = db.hoa_don_chi_tiet.Count();
            return PartialView("_Overview", db.ve_ban.ToList());
        }

        public ActionResult Render_Phim()
        {
            var select = from m in db.ve_ban
                         group m by m.suat_chieu.phim_id into phim
                         select new TKPhim
                         {
                             total = (from x in db.ve_ban select x)
                             .Where(x => x.trang_thai.Equals("Sold") && x.suat_chieu.phim_id == phim.Key)
                             .Sum(x => x.tong__tien) ?? default(int),
                             book = (from x in db.ve_ban select x)
                             .Where(x => x.trang_thai.Equals("Book") && x.suat_chieu.phim_id == phim.Key)
                             .Sum(x => x.tong__tien) ?? default(int),
                             sl_sold = (from x in db.ve_ban select x)
                             .Where(x => x.trang_thai.Equals("Sold") && x.suat_chieu.phim_id == phim.Key)
                             .Count(),
                             sl_book = (from x in db.ve_ban select x)
                             .Where(x => x.trang_thai.Equals("Book") && x.suat_chieu.phim_id == phim.Key)
                             .Count(),
                             ten = db.phim.Where(x => x.id == phim.Key).Select(x => x.ten).FirstOrDefault(),
                             link = db.phim.Where(x => x.id == phim.Key).Select(x => x.hinh_anh).FirstOrDefault(),
                         };
            var list = select.OrderByDescending(x => x.total).Take(9).ToList();
            ViewBag.ListPhim = list;
            ViewBag.ReturnCount = list.Count();

            return PartialView("_Phim");
        }

        public ActionResult Render_Food()
        {
            var select = from m in db.hoa_don_chi_tiet
                         group m by m.do_an_chi_tiet_id into food
                         select new TKFood
                         {
                             tong = (from x in db.hoa_don_chi_tiet select x)
                             .Where(x => x.do_an_chi_tiet_id == food.Key)
                             .Sum(x => x.tong_tien) ?? default(int),
                             sl = (from x in db.hoa_don_chi_tiet select x)
                             .Where(x => x.do_an_chi_tiet_id == food.Key)
                             .Sum(x => x.so_luong) ?? default(int),
                             ten = db.do_an_chi_tiet.Where(x => x.id == food.Key).Select(x => x.do_an.ten + " (" + x.kich_co_do_an.ten + ")").FirstOrDefault(),
                         };
            var list = select.OrderByDescending(x => x.tong).Take(9).ToList();
            ViewBag.ListFood = list;
            ViewBag.ReturnCount = list.Count();
            return PartialView("_Food");
        }
    }
}
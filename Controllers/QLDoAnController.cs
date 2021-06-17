using PagedList;
using PagedList.Mvc;
using QLBanVePhim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanVePhim.Controllers
{
    public class QLDoAnController : Controller
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

        public ActionResult Index(string tenDA, string loaiDoAn, int? page)
        {
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index", "QLHome");
            var _doAn = db.do_an.ToList();
            if (!String.IsNullOrEmpty(tenDA))
                _doAn = db.do_an.Where(s => s.ten.ToLower().Contains(tenDA.ToLower())).ToList();
            if (!String.IsNullOrEmpty(loaiDoAn))
            {
                _doAn = db.do_an.Where(s => s.loai_do_an_id == loaiDoAn).ToList();
            }
            var loaiDA = db.loai_do_an.ToList();
            ViewBag.LoaiDoAn = new SelectList(loaiDA, "id", "ten");
            ViewBag.CurrTen = tenDA;
            ViewBag.CurrLF = loaiDoAn;
            int pageNum = (page ?? 1);
            return View(_doAn.ToPagedList(pageNum, pageSize));
        }

        #region Đồ Ăn

        public ActionResult AddDoAn()
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLDoAn");
            var loaiDoAn = db.loai_do_an.ToList();
            ViewBag.LoaiDoAn = new SelectList(loaiDoAn, "id", "ten");
            var sizeDoAn = db.kich_co_do_an.ToList();
            ViewBag.Size = new SelectList(sizeDoAn, "id", "ten");
            do_an_chi_tiet doAn = new do_an_chi_tiet();
            return View(doAn);
        }

        [HttpPost]
        public ActionResult AddDoAn(do_an_chi_tiet _dact)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLDoAn");
            try
            {
                do_an _da = new do_an
                {
                    id = _dact.do_an.id,
                    ten = _dact.do_an.ten,
                    dang_ban = true,
                    hinh_anh = _dact.do_an.hinh_anh,
                    loai_do_an_id = _dact.do_an.loai_do_an_id
                };
                _dact.do_an_id = _da.id;
                _dact.id = _da.id + "_" + _dact.kich_co_do_an_id;
                _dact.dang_ban = true;
                _dact.do_an.dang_ban = true;
                db.do_an_chi_tiet.Add(_dact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return View("~/Views/QLHome/Error.cshtml");
            }
        }

        public ActionResult Render_SizeList(string id)
        {
            var sizeList = db.kich_co_do_an.ToList();
            var sizeGot = db.do_an_chi_tiet
                .Where(s => s.do_an_id == id)
                .Select(s => s.kich_co_do_an_id)
                .ToList();
            var sizeGot_Price = db.do_an_chi_tiet
                .Where(s => s.do_an_id == id)
                .Select(s => s.don_gia)
                .ToList();
            ViewBag.ID = id;
            ViewBag.SizeGot = sizeGot;
            ViewBag.SizeGotPrice = sizeGot_Price;
            return PartialView("_SizeList", sizeList.ToList());
        }

        public ActionResult Render_ChiTietList(string id)
        {
            var dactList = db.do_an_chi_tiet.Where(s => s.do_an.id == id).ToList();
            ViewBag.ID = id;
            return PartialView("_ChiTietList", dactList);
        }

        public ActionResult EditDoAn(string id)
        {
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index", "QLHome");
            var dact = db.do_an_chi_tiet.Where(s => s.do_an.id == id).ToList();
            ViewBag.DACT = dact;
            var loaiDoAn = db.loai_do_an.ToList();
            ViewBag.ID = id;
            ViewBag.LoaiDoAn = new SelectList(loaiDoAn, "id", "ten");
            return View(db.do_an.Where(s => s.id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditDoAn(string id, do_an _da)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLDoAn");
            db.Entry(_da).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateGia(FormCollection form)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLDoAn");
            string id = form["idDACT"].ToString();
            do_an_chi_tiet dact = db.do_an_chi_tiet.Where(item => item.id == id).FirstOrDefault();
            if (String.IsNullOrEmpty(form["DonGia"]) || int.Parse(form["DonGia"]) < 0)
                dact.don_gia = 0;
            else
                dact.don_gia = int.Parse(form["DonGia"]);
            db.SaveChanges();
            return RedirectToAction("EditDoAn", "QLDoAn", new { id = dact.do_an_id });
        }

        public ActionResult AppendSize(string id, string size)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLDoAn");
            try
            {
                do_an _da = db.do_an.Where(s => s.id == id).FirstOrDefault();
                do_an_chi_tiet _dact = new do_an_chi_tiet
                {
                    do_an_id = _da.id,
                    id = _da.id + "_" + size,
                    dang_ban = true,
                    don_gia = 0,
                    kich_co_do_an_id = size
                };
                var _check = db.do_an_chi_tiet.Where(s => s.do_an_id == id && s.kich_co_do_an_id == size).ToList();
                if (_check.Count() == 0)
                {
                    db.do_an_chi_tiet.Add(_dact);
                    db.SaveChanges();
                }
                return RedirectToAction("EditDoAn", "QLDoAn", new { id = id });
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return View("~/Views/QLHome/Error.cshtml");
            }
        }

        public ActionResult PopSize(string id, string size)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLDoAn");
            try
            {
                var _dact = db.do_an_chi_tiet.Where(s => s.do_an_id == id && s.kich_co_do_an_id == size).FirstOrDefault();
                var _check = db.do_an_chi_tiet.Where(s => s.do_an_id == id && s.kich_co_do_an_id == size).ToList();
                if (_check.Count() == 1)
                {
                    db.Entry(_dact).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                return RedirectToAction("EditDoAn", "QLDoAn", new { id = id });
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return View("~/Views/QLHome/Error.cshtml");
            }
        }

        public ActionResult DeleteDoAn(string id, do_an _doan)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLDoAn");
            try
            {
                _doan = db.do_an.Where(item => item.id == id).FirstOrDefault();
                db.do_an.Remove(_doan);
                var _dact = db.do_an_chi_tiet.Where(item => item.do_an_id == id).ToList();
                foreach (var dact in _dact)
                {
                    db.do_an_chi_tiet.Remove(dact);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return View("~/Views/QLHome/Error.cshtml");
            }
        }

        #endregion Đồ Ăn

        #region Hóa Đơn

        public ActionResult HoaDon(int? page)
        {
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index", "QLHome");
            var _hoadon = db.hoa_don.ToList();
            int pageNum = (page ?? 1);
            return View(_hoadon.ToPagedList(pageNum, pageSize));
        }

        public ActionResult AddHD()
        {
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index", "QLHome");
            var _hoadon = new hoa_don
            {
                id = DateTime.Now.ToString("ddMMyy") + "_" + DateTime.Now.ToString("HHmmss"),
                ngay_ban = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                nhan_vien_id = int.Parse(Session["Id_Admin"].ToString())
            };
            db.hoa_don.Add(_hoadon);
            db.SaveChanges();
            return RedirectToAction("AddHoaDon", "QLDoAn", new { id = _hoadon.id });
        }

        public ActionResult AddHoaDon(string id)
        {
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index", "QLHome");
            ViewBag.ID = id;
            var _hd = db.hoa_don.Where(s => s.id == id).FirstOrDefault();
            ViewBag.TongTien = Convert.ToInt32(db.hoa_don_chi_tiet.Where(s => s.hoa_don_id == id).Sum(s => s.tong_tien));
            return View(_hd);
        }

        public ActionResult Render_HDCT(string id)
        {
            ViewBag.ID = id;
            var _hdct = db.hoa_don_chi_tiet.Where(s => s.hoa_don_id == id).ToList();
            return PartialView("_HDCT", _hdct);
        }

        public ActionResult Render_HDAdd(string id)
        {
            ViewBag.ID = id;
            var dactList = db.do_an_chi_tiet.Select(s => new
            {
                id = s.id,
                ten_dact = s.do_an.ten + " (" + s.kich_co_do_an.ten + ") - " + s.don_gia + " VND"
            }).ToList();
            ViewBag.DACTList = new SelectList(dactList, "id", "ten_dact");
            var priceList = db.do_an_chi_tiet.ToList();
            ViewBag.PriceList = new SelectList(priceList, "id", "don_gia");
            return PartialView("_HDAdd");
        }

        [HttpPost]
        public ActionResult HDAddDoAn(hoa_don_chi_tiet _hdct)
        {
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index", "QLDoAn");
            try
            {
                Random random = new Random();
                string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm";
                string randomChar = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());

                var _dact = db.do_an_chi_tiet.Where(s => s.id == _hdct.do_an_chi_tiet_id).FirstOrDefault();

                _hdct.id = _hdct.hoa_don_id + "-" + randomChar;
                _hdct.tong_tien = _dact.don_gia * _hdct.so_luong;
                db.hoa_don_chi_tiet.Add(_hdct);
                db.SaveChanges();
                ViewBag.ID = _hdct.hoa_don_id;
                return RedirectToAction("AddHoaDon", "QLDoAn", new { id = _hdct.hoa_don_id });
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return View("~/Views/QLHome/Error.cshtml");
            }
        }

        [HttpPost]
        public ActionResult UpdateHDCT(FormCollection form)
        {
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index", "QLDoAn");
            string id = form["idHDCT"].ToString();
            hoa_don_chi_tiet _hdct = db.hoa_don_chi_tiet.Where(item => item.id == id).FirstOrDefault();
            if (String.IsNullOrEmpty(form["SoLuong"]) || int.Parse(form["SoLuong"]) < 1)
                _hdct.so_luong = 1;
            else
                _hdct.so_luong = int.Parse(form["SoLuong"]);
            _hdct.tong_tien = _hdct.so_luong * _hdct.do_an_chi_tiet.don_gia;
            db.SaveChanges();
            return RedirectToAction("AddHoaDon", "QLDoAn", new { id = _hdct.hoa_don_id });
        }

        public ActionResult DeleteHDCT(string id)
        {
            if (!AuthCheck("nhanvien"))
                return RedirectToAction("Index", "QLDoAn");
            hoa_don_chi_tiet _hdct = db.hoa_don_chi_tiet.Where(item => item.id == id).FirstOrDefault();
            string hdid = _hdct.hoa_don_id;
            ViewBag.ID = hdid;
            db.Entry(_hdct).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("AddHoaDon", "QLDoAn", new { id = hdid });
        }

        public ActionResult HuyHoaDon(string id)
        {
            if (!AuthCheck("admin"))
                return RedirectToAction("Index", "QLDoAn");
            try
            {
                var hdct = db.hoa_don_chi_tiet.Where(i => i.hoa_don_id == id).ToList();
                hoa_don _hd = db.hoa_don.Where(item => item.id == id).FirstOrDefault();
                foreach (var item in hdct)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
                db.Entry(_hd).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("HoaDon");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return View("~/Views/QLHome/Error.cshtml");
            }
        }

        #endregion Hóa Đơn
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanVePhim.Models;


namespace QLBanVePhim.Controllers
{
    
    public class QLHomeController : Controller
    {
        QLBanVePhimEntities db = new QLBanVePhimEntities();
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
        public ActionResult QLSuatChieu()
        {
            return View();
        }

        public ActionResult QLPhongChieu()
        {
            return View();
        }
        public ActionResult AddPhongChieu()
        {
            return View();
        }

        public ActionResult EditPhongChieu()
        {
            return View();
        }

        public ActionResult QLVe()
        {
            return View();
        }
    }
}
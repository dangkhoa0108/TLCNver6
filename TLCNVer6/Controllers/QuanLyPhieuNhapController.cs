﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLCNVer6.Models;
using TLCNVer6.ViewModel;

namespace TLCNVer6.Controllers
{
    [PhanQuyen]
    public class QuanLyPhieuNhapController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();


        // GET: QuanLyPhieuNhap
        public ActionResult Index()
        { 
            if(Session["Username"]!=null)
            {
                ViewBag.DonViGN = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV");
                ViewBag.Kho = new SelectList(db.Khoes, "MaKho", "TenKho");
                ViewBag.MatHang = new SelectList(db.MatHangs, "MaMatHang", "TenMatHang");
                return View();
            }
            else
            {
                return Redirect("~/Login/Index");
            }
            
        }


        [HttpPost]
        public JsonResult SaveOrder(PhieuNhapViewModel O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (QuanLyKhoDuocPhamDbContext dc = new QuanLyKhoDuocPhamDbContext())
                {
                    ThongTinPN order = new ThongTinPN { MaPN = O.MaPN, NgayLap = O.NgayLap, GiaTriDen = O.GiaTriDen, NguoiLap = O.NguoiLap, MaDonVi=O.MaDV };
                    foreach (var i in O.ChiTietPN)
                    {
                        //
                        // i.TotalAmount = 
                        order.ChiTietPNs.Add(i);
                    }
                    order.NguoiLap = Session["IDU"].ToString();
                    dc.ThongTinPNs.Add(order);
                    dc.SaveChanges();
                    status = true;
                }
            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }
 
    }
}

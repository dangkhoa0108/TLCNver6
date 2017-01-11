using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLCNVer6.Models;
using TLCNVer6.ViewModel;

namespace TLCNVer6.Controllers
{
    public class QuanLyPhieuNhapController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();


        // GET: QuanLyPhieuNhap
        public ActionResult Index()
        {
            ViewBag.DonViGN = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV");
            ViewBag.Kho = new SelectList(db.Khoes, "MaKho", "TenKho");
            ViewBag.MatHang = new SelectList(db.MatHangs, "MaMatHang", "TenMatHang");
            return View();
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

    // GET: QuanLyPhieuNhap/Details/5
    public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuanLyPhieuNhap/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanLyPhieuNhap/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QuanLyPhieuNhap/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuanLyPhieuNhap/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QuanLyPhieuNhap/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuanLyPhieuNhap/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLCNVer6.Models;
using TLCNVer6.ViewModel;

namespace TLCNVer6.Controllers
{
    public class QuanLyPhieuXuatController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: QuanLyPhieuXuat
        public ActionResult Index()
        {
            ViewBag.DonViGN = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV");
            ViewBag.Kho = new SelectList(db.Khoes, "MaKho", "TenKho");
            ViewBag.MatHang = new SelectList(db.MatHangs, "MaMatHang", "TenMatHang");
            return View();
        }

        [HttpPost]
        public JsonResult SaveOrder(PhieuXuatViewModel O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (QuanLyKhoDuocPhamDbContext dc = new QuanLyKhoDuocPhamDbContext())
                {
                    ThongTinPX order = new ThongTinPX { MaPX = O.MaPX, NgayLap = O.NgayLap, GiaTriDen = O.GiaTriDen, NguoiLap = O.NguoiLap, MaKho = O.MaKho };
                    foreach (var i in O.ChiTietPX)
                    {
                        //
                        // i.TotalAmount = 
                        order.ChiTietPXes.Add(i);
                    }
                    dc.ThongTinPXes.Add(order);
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

        // GET: QuanLyPhieuXuat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuanLyPhieuXuat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanLyPhieuXuat/Create
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

        // GET: QuanLyPhieuXuat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuanLyPhieuXuat/Edit/5
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

        // GET: QuanLyPhieuXuat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuanLyPhieuXuat/Delete/5
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

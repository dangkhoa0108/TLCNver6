using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLCNVer6.Models;
using TLCNVer6.ViewModel;

namespace TLCNVer6.Controllers
{
    public class QuanLyHopDongController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();
        // GET: QuanLyHopDong
        public ActionResult Index()
        {
            ViewBag.DonViGN = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV");
            ViewBag.Kho = new SelectList(db.Khoes, "MaKho", "TenKho");
            ViewBag.MatHang = new SelectList(db.MatHangs, "MaMatHang", "TenMatHang");
            return View();
        }

        [HttpPost]
        public JsonResult SaveOrder(HopDongViewModel O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (QuanLyKhoDuocPhamDbContext dc = new QuanLyKhoDuocPhamDbContext())
                {
                    ThongTinHopDong order = new ThongTinHopDong { MaHD = O.MaHD, TinhChat = O.TinhChat, MaKho = O.MaKho, MaDV = O.MaDV, NgayKi = O.NgayKi, NguoiLap=O.NguoiLap };
                    foreach (var i in O.ChiTietHD)
                    {
                        //
                        // i.TotalAmount = 
                        order.ChiTietHDs.Add(i);
                    }
                    dc.ThongTinHopDongs.Add(order);
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

        // GET: QuanLyHopDong/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuanLyHopDong/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanLyHopDong/Create
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

        // GET: QuanLyHopDong/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuanLyHopDong/Edit/5
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

        // GET: QuanLyHopDong/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuanLyHopDong/Delete/5
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

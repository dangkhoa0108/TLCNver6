using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TLCNVer6.Models;
using TLCNVer6.ViewModel;

namespace TLCNVer6.Controllers
{
    public class QuanLyMatHangController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: QuanLyMatHang
        public ActionResult Index()
        {
            var matHangs = db.MatHangs.Include(m => m.Kho).Include(m => m.LoaiMatHang);
            return View(matHangs.ToList());
        }

        public ActionResult Find()
        {
            ViewBag.Kho = new SelectList(db.Khoes, "MaKho", "TenKho");
            ViewBag.LoaiMH = new SelectList(db.LoaiMatHangs, "MaLoaiMH", "TenLoaiMH");
            return View();
        }


        [HttpPost]
        public ActionResult Details(string Kho, string LoaiMH)
        {
            List<KiemKeHangHoaViewModel> model = new List<KiemKeHangHoaViewModel>();
            var join = (from K in db.Khoes
                        join MH in db.MatHangs
                        on K.MaKho equals MH.MaKho
                        join LMH in db.LoaiMatHangs on
                            MH.MaLoaiMH equals LMH.MaLoaiMH
                        where
                            (K.MaKho == Kho) && (LMH.MaLoaiMH == LoaiMH)
                        select new
                        {
                            maMH = MH.MaMatHang,
                            tenMH = MH.TenMatHang,
                            tenLMH = LMH.TenLoaiMH,
                            donViTinh = MH.DonViTinh,
                            tenKho = K.TenKho,
                            soLuong = MH.SoLuong
                        }).ToList();
            foreach(var item in join)
            {
                model.Add(new KiemKeHangHoaViewModel()
                {
                    MaMatHang = item.maMH,
                    TenMatHang = item.tenMH,
                    TenLoaiMatHang = item.tenLMH,
                    DonViTinh = item.donViTinh,
                    TenKho = item.tenKho,
                    SoLuong = item.soLuong
                });
            }
            return View(model);
        }

        // GET: QuanLyMatHang/Create
        public ActionResult Create()
        {
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho");
            ViewBag.MaLoaiMH = new SelectList(db.LoaiMatHangs, "MaLoaiMH", "TenLoaiMH");
            return View();
        }

        // POST: QuanLyMatHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMatHang,MaLoaiMH,TenMatHang,DonViTinh,MaKho,SoLuong")] MatHang matHang)
        {
            if (ModelState.IsValid)
            {
                db.MatHangs.Add(matHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", matHang.MaKho);
            ViewBag.MaLoaiMH = new SelectList(db.LoaiMatHangs, "MaLoaiMH", "TenLoaiMH", matHang.MaLoaiMH);
            return View(matHang);
        }

        // GET: QuanLyMatHang/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatHang matHang = db.MatHangs.Find(id);
            if (matHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", matHang.MaKho);
            ViewBag.MaLoaiMH = new SelectList(db.LoaiMatHangs, "MaLoaiMH", "TenLoaiMH", matHang.MaLoaiMH);
            return View(matHang);
        }

        // POST: QuanLyMatHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMatHang,MaLoaiMH,TenMatHang,DonViTinh,MaKho,SoLuong")] MatHang matHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", matHang.MaKho);
            ViewBag.MaLoaiMH = new SelectList(db.LoaiMatHangs, "MaLoaiMH", "TenLoaiMH", matHang.MaLoaiMH);
            return View(matHang);
        }

        // GET: QuanLyMatHang/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatHang matHang = db.MatHangs.Find(id);
            if (matHang == null)
            {
                return HttpNotFound();
            }
            return View(matHang);
        }

        // POST: QuanLyMatHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MatHang matHang = db.MatHangs.Find(id);
            db.MatHangs.Remove(matHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

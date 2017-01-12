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
    [PhanQuyen]
    public class QuanLyKhoController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: QuanLyKho
        public ActionResult Index()
        {
            if(Session["Username"]!=null)
            {
                var khoes = db.Khoes.Include(k => k.LoaiKho);
                return View(khoes.ToList());
            }
            else
            {
                return Redirect("~/Login/Index");
            }
            
        }

        public ActionResult Find()
        {
            ViewBag.Kho = new SelectList(db.Khoes, "MaKho", "TenKho");
            return View();
        }

        [HttpPost]
        public ActionResult Details(string Kho)
        {
            List<KiemKeHangHoaViewModel> model = new List<KiemKeHangHoaViewModel>();
            var join = (from K in db.Khoes
                        join MH in db.MatHangs
                        on K.MaKho equals MH.MaKho
                        join LMH in db.LoaiMatHangs on
                            MH.MaLoaiMH equals LMH.MaLoaiMH
                        where
                            (K.MaKho == Kho)
                        select new
                        {
                            maMH = MH.MaMatHang,
                            tenMH = MH.TenMatHang,
                            tenLMH = LMH.TenLoaiMH,
                            donViTinh = MH.DonViTinh,
                            tenKho = K.TenKho,
                            soLuong = MH.SoLuong
                        }).ToList();
            foreach (var item in join)
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

        // GET: QuanLyKho/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiKho = new SelectList(db.LoaiKhoes, "MaLoaiKho", "TenLoaiKho");
            return View();
        }

        // POST: QuanLyKho/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKho,TenKho,MaLoaiKho")] Kho kho)
        {
            if (ModelState.IsValid)
            {
                db.Khoes.Add(kho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiKho = new SelectList(db.LoaiKhoes, "MaLoaiKho", "TenLoaiKho", kho.MaLoaiKho);
            return View(kho);
        }

        // GET: QuanLyKho/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kho kho = db.Khoes.Find(id);
            if (kho == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiKho = new SelectList(db.LoaiKhoes, "MaLoaiKho", "TenLoaiKho", kho.MaLoaiKho);
            return View(kho);
        }

        // POST: QuanLyKho/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKho,TenKho,MaLoaiKho")] Kho kho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiKho = new SelectList(db.LoaiKhoes, "MaLoaiKho", "TenLoaiKho", kho.MaLoaiKho);
            return View(kho);
        }

        // GET: QuanLyKho/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kho kho = db.Khoes.Find(id);
            if (kho == null)
            {
                return HttpNotFound();
            }
            return View(kho);
        }

        // POST: QuanLyKho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Kho kho = db.Khoes.Find(id);
                db.Khoes.Remove(kho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return Redirect("~/Login/DelError");
            }
            
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

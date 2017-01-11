using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TLCNVer6.Models;

namespace TLCNVer6.Controllers
{
    public class QuanLyKhoController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: QuanLyKho
        public ActionResult Index()
        {
            var khoes = db.Khoes.Include(k => k.LoaiKho);
            return View(khoes.ToList());
        }

        // GET: QuanLyKho/Details/5
        public ActionResult Details(string id)
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
            Kho kho = db.Khoes.Find(id);
            db.Khoes.Remove(kho);
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

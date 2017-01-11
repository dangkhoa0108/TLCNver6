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
    public class QuanLyLoaiKhoController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: QuanLyLoaiKho
        public ActionResult Index()
        {
            return View(db.LoaiKhoes.ToList());
        }

        // GET: QuanLyLoaiKho/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiKho loaiKho = db.LoaiKhoes.Find(id);
            if (loaiKho == null)
            {
                return HttpNotFound();
            }
            return View(loaiKho);
        }

        // GET: QuanLyLoaiKho/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanLyLoaiKho/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiKho,TenLoaiKho")] LoaiKho loaiKho)
        {
            if (ModelState.IsValid)
            {
                db.LoaiKhoes.Add(loaiKho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiKho);
        }

        // GET: QuanLyLoaiKho/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiKho loaiKho = db.LoaiKhoes.Find(id);
            if (loaiKho == null)
            {
                return HttpNotFound();
            }
            return View(loaiKho);
        }

        // POST: QuanLyLoaiKho/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiKho,TenLoaiKho")] LoaiKho loaiKho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiKho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiKho);
        }

        // GET: QuanLyLoaiKho/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiKho loaiKho = db.LoaiKhoes.Find(id);
            if (loaiKho == null)
            {
                return HttpNotFound();
            }
            return View(loaiKho);
        }

        // POST: QuanLyLoaiKho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LoaiKho loaiKho = db.LoaiKhoes.Find(id);
            db.LoaiKhoes.Remove(loaiKho);
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

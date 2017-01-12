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
    [PhanQuyen]
    public class QuanLyLoaiMatHangController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: QuanLyLoaiMatHang
        public ActionResult Index()
        {
            if(Session["Username"]!=null)
            {
                return View(db.LoaiMatHangs.ToList());
            }
            else
            {
                return Redirect("~/Login/Index");
            }
        }

        // GET: QuanLyLoaiMatHang/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiMatHang loaiMatHang = db.LoaiMatHangs.Find(id);
            if (loaiMatHang == null)
            {
                return HttpNotFound();
            }
            return View(loaiMatHang);
        }

        // GET: QuanLyLoaiMatHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanLyLoaiMatHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiMH,TenLoaiMH")] LoaiMatHang loaiMatHang)
        {
            if (ModelState.IsValid)
            {
                db.LoaiMatHangs.Add(loaiMatHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiMatHang);
        }

        // GET: QuanLyLoaiMatHang/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiMatHang loaiMatHang = db.LoaiMatHangs.Find(id);
            if (loaiMatHang == null)
            {
                return HttpNotFound();
            }
            return View(loaiMatHang);
        }

        // POST: QuanLyLoaiMatHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiMH,TenLoaiMH")] LoaiMatHang loaiMatHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiMatHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiMatHang);
        }

        // GET: QuanLyLoaiMatHang/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiMatHang loaiMatHang = db.LoaiMatHangs.Find(id);
            if (loaiMatHang == null)
            {
                return HttpNotFound();
            }
            return View(loaiMatHang);
        }

        // POST: QuanLyLoaiMatHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                LoaiMatHang loaiMatHang = db.LoaiMatHangs.Find(id);
                db.LoaiMatHangs.Remove(loaiMatHang);
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

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
    public class ThongTinPhieuXuatController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: ThongTinPhieuXuat
        public ActionResult Index()
        {
            var thongTinPXes = db.ThongTinPXes.Include(t => t.Kho).Include(t => t.Login);
            return View(thongTinPXes.ToList());
        }

        // GET: ThongTinPhieuXuat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinPX thongTinPX = db.ThongTinPXes.Find(id);
            if (thongTinPX == null)
            {
                return HttpNotFound();
            }
            return View(thongTinPX);
        }

        // GET: ThongTinPhieuXuat/Create
        public ActionResult Create()
        {
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho");
            ViewBag.NguoiLap = new SelectList(db.Logins, "ID", "Username");
            return View();
        }

        // POST: ThongTinPhieuXuat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaPX,NgayLap,GiaTriDen,NguoiLap,MaKho")] ThongTinPX thongTinPX)
        {
            if (ModelState.IsValid)
            {
                db.ThongTinPXes.Add(thongTinPX);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", thongTinPX.MaKho);
            ViewBag.NguoiLap = new SelectList(db.Logins, "ID", "Username", thongTinPX.NguoiLap);
            return View(thongTinPX);
        }

        // GET: ThongTinPhieuXuat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinPX thongTinPX = db.ThongTinPXes.Find(id);
            if (thongTinPX == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", thongTinPX.MaKho);
            ViewBag.NguoiLap = new SelectList(db.Logins, "ID", "Username", thongTinPX.NguoiLap);
            return View(thongTinPX);
        }

        // POST: ThongTinPhieuXuat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaPX,NgayLap,GiaTriDen,NguoiLap,MaKho")] ThongTinPX thongTinPX)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongTinPX).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", thongTinPX.MaKho);
            ViewBag.NguoiLap = new SelectList(db.Logins, "ID", "Username", thongTinPX.NguoiLap);
            return View(thongTinPX);
        }

        // GET: ThongTinPhieuXuat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinPX thongTinPX = db.ThongTinPXes.Find(id);
            if (thongTinPX == null)
            {
                return HttpNotFound();
            }
            return View(thongTinPX);
        }

        // POST: ThongTinPhieuXuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongTinPX thongTinPX = db.ThongTinPXes.Find(id);
            db.ThongTinPXes.Remove(thongTinPX);
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

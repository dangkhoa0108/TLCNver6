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
    public class ThongTinPhieuNhapController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: ThongTinPhieuNhap
        public ActionResult Index()
        {
            var thongTinPNs = db.ThongTinPNs.Include(t => t.DonViGiaoNhan).Include(t => t.Login);
            return View(thongTinPNs.ToList());
        }

        // GET: ThongTinPhieuNhap/Details/5
        public ActionResult Details(string MaPN)
        {
          
            return View();
        }

        public ActionResult LocTheoDonVi()
        {
            ViewBag.DonVi = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV");
            return View();
        }


        [HttpPost]
        public ActionResult Export()
        {

        }

        // GET: ThongTinPhieuNhap/Create
        public ActionResult Create()
        {
            ViewBag.MaDonVi = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV");
            ViewBag.NguoiLap = new SelectList(db.Logins, "ID", "Username");
            return View();
        }

        // POST: ThongTinPhieuNhap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaPN,NgayLap,GiaTriDen,NguoiLap,MaDonVi")] ThongTinPN thongTinPN)
        {
            if (ModelState.IsValid)
            {
                db.ThongTinPNs.Add(thongTinPN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDonVi = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV", thongTinPN.MaDonVi);
            ViewBag.NguoiLap = new SelectList(db.Logins, "ID", "Username", thongTinPN.NguoiLap);
            return View(thongTinPN);
        }

        // GET: ThongTinPhieuNhap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinPN thongTinPN = db.ThongTinPNs.Find(id);
            if (thongTinPN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDonVi = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV", thongTinPN.MaDonVi);
            ViewBag.NguoiLap = new SelectList(db.Logins, "ID", "Username", thongTinPN.NguoiLap);
            return View(thongTinPN);
        }

        // POST: ThongTinPhieuNhap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaPN,NgayLap,GiaTriDen,NguoiLap,MaDonVi")] ThongTinPN thongTinPN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongTinPN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDonVi = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV", thongTinPN.MaDonVi);
            ViewBag.NguoiLap = new SelectList(db.Logins, "ID", "Username", thongTinPN.NguoiLap);
            return View(thongTinPN);
        }

        // GET: ThongTinPhieuNhap/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinPN thongTinPN = db.ThongTinPNs.Find(id);
            if (thongTinPN == null)
            {
                return HttpNotFound();
            }
            return View(thongTinPN);
        }

        // POST: ThongTinPhieuNhap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongTinPN thongTinPN = db.ThongTinPNs.Find(id);
            db.ThongTinPNs.Remove(thongTinPN);
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

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
    public class ThongTinHopDongController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: ThongTinHopDong
        public ActionResult Index()
        {
            if(Session["Username"]!=null)
            {
                var thongTinHopDongs = db.ThongTinHopDongs.Include(t => t.DonViGiaoNhan).Include(t => t.DonViGiaoNhan1).Include(t => t.Kho).Include(t => t.Login);
                return View(thongTinHopDongs.ToList());
            }
            else
            {
                return Redirect("~/Login/Index");
            }
            
        }

        // GET: ThongTinHopDong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinHopDong thongTinHopDong = db.ThongTinHopDongs.Find(id);
            if (thongTinHopDong == null)
            {
                return HttpNotFound();
            }
            return View(thongTinHopDong);
        }

        // GET: ThongTinHopDong/Create
        public ActionResult Create()
        {
            ViewBag.MaDV = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV");
            ViewBag.MaDV = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV");
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho");
            ViewBag.NguoiLap = new SelectList(db.Logins, "ID", "Username");
            return View();
        }

        // POST: ThongTinHopDong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaHD,TinhChat,MaKho,MaDV,NgayKi,NguoiLap")] ThongTinHopDong thongTinHopDong)
        {
            if (ModelState.IsValid)
            {
                db.ThongTinHopDongs.Add(thongTinHopDong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDV = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV", thongTinHopDong.MaDV);
            ViewBag.MaDV = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV", thongTinHopDong.MaDV);
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", thongTinHopDong.MaKho);
            ViewBag.NguoiLap = new SelectList(db.Logins, "ID", "Username", thongTinHopDong.NguoiLap);
            return View(thongTinHopDong);
        }

        // GET: ThongTinHopDong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinHopDong thongTinHopDong = db.ThongTinHopDongs.Find(id);
            if (thongTinHopDong == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDV = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV", thongTinHopDong.MaDV);
            ViewBag.MaDV = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV", thongTinHopDong.MaDV);
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", thongTinHopDong.MaKho);
            ViewBag.NguoiLap = new SelectList(db.Logins, "ID", "Username", thongTinHopDong.NguoiLap);
            return View(thongTinHopDong);
        }

        // POST: ThongTinHopDong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaHD,TinhChat,MaKho,MaDV,NgayKi,NguoiLap")] ThongTinHopDong thongTinHopDong)
        {
            if (ModelState.IsValid)
            {

                db.Entry(thongTinHopDong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDV = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV", thongTinHopDong.MaDV);
            ViewBag.MaDV = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV", thongTinHopDong.MaDV);
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", thongTinHopDong.MaKho);
            ViewBag.NguoiLap = new SelectList(db.Logins, "ID", "Username", thongTinHopDong.NguoiLap);
            return View(thongTinHopDong);
        }

        // GET: ThongTinHopDong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinHopDong thongTinHopDong = db.ThongTinHopDongs.Find(id);
            if (thongTinHopDong == null)
            {
                return HttpNotFound();
            }
            return View(thongTinHopDong);
        }

        // POST: ThongTinHopDong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ThongTinHopDong thongTinHopDong = db.ThongTinHopDongs.Find(id);
                db.ThongTinHopDongs.Remove(thongTinHopDong);
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

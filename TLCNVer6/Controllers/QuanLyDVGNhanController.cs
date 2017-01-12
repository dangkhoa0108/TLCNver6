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
    public class QuanLyDVGNhanController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: QuanLyDVGNhan
        public ActionResult Index()
        {
            if(Session["Username"]!=null)
            {
                return View(db.DonViGiaoNhans.ToList());
            }
            else
            {
                return Redirect("~/Login/Index");
            }
        }

        // GET: QuanLyDVGNhan/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonViGiaoNhan donViGiaoNhan = db.DonViGiaoNhans.Find(id);
            if (donViGiaoNhan == null)
            {
                return HttpNotFound();
            }
            return View(donViGiaoNhan);
        }

        // GET: QuanLyDVGNhan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanLyDVGNhan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDV,TenDV")] DonViGiaoNhan donViGiaoNhan)
        {
            if (ModelState.IsValid)
            {
                db.DonViGiaoNhans.Add(donViGiaoNhan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donViGiaoNhan);
        }

        // GET: QuanLyDVGNhan/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonViGiaoNhan donViGiaoNhan = db.DonViGiaoNhans.Find(id);
            if (donViGiaoNhan == null)
            {
                return HttpNotFound();
            }
            return View(donViGiaoNhan);
        }

        // POST: QuanLyDVGNhan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDV,TenDV")] DonViGiaoNhan donViGiaoNhan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donViGiaoNhan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donViGiaoNhan);
        }

        // GET: QuanLyDVGNhan/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonViGiaoNhan donViGiaoNhan = db.DonViGiaoNhans.Find(id);
            if (donViGiaoNhan == null)
            {
                return HttpNotFound();
            }
            return View(donViGiaoNhan);
        }

        // POST: QuanLyDVGNhan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                DonViGiaoNhan donViGiaoNhan = db.DonViGiaoNhans.Find(id);
                db.DonViGiaoNhans.Remove(donViGiaoNhan);
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

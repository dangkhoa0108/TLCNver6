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
    public class ChiTietHopDongController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: ChiTietHopDong
        public ActionResult Index()
        {
            var chiTietHDs = db.ChiTietHDs.Include(c => c.MatHang).Include(c => c.ThongTinHopDong);
            return View(chiTietHDs.ToList());
        }

        // GET: ChiTietHopDong/Details/5
        public ActionResult Details(int? ID)
        {
            Session["ID"] = ID.ToString();
            List<ChiTietHopDongViewModel> model = new List<ChiTietHopDongViewModel>();
            var join = (from ThongTinHopDong in db.ThongTinHopDongs
                        join
                        ChiTietHD in db.ChiTietHDs on ThongTinHopDong.ID equals ChiTietHD.IDHD
                        join MatHang in db.MatHangs on ChiTietHD.MaMatHang equals MatHang.MaMatHang
                        where (ThongTinHopDong.ID == ID)
                        select new
                        {
                            iD = ChiTietHD.ID,
                            idHD = ThongTinHopDong.MaHD,
                            tenMH = MatHang.TenMatHang,
                            soLuong = ChiTietHD.SoLuong,
                            donGia = ChiTietHD.DonGia,
                            sum = ChiTietHD.Sum
                        }).ToList();
            foreach (var item in join)
            {
                model.Add(new ChiTietHopDongViewModel()
                {
                    ID = item.iD,
                    MaHD = item.idHD,
                    TenMatHang = item.tenMH,
                    SoLuong = item.soLuong,
                    DonGia = item.donGia,
                    Tong = item.sum
                });
            }
            return View(model);
        }

        // GET: ChiTietHopDong/Create
        public ActionResult Create()
        {
            ViewBag.MaMatHang = new SelectList(db.MatHangs, "MaMatHang", "MaLoaiMH");
            ViewBag.IDHD = new SelectList(db.ThongTinHopDongs, "ID", "MaHD");
            return View();
        }

        // POST: ChiTietHopDong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDHD,MaMatHang,SoLuong,DonGia,Sum")] ChiTietHD chiTietHD)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["ID"]);
                chiTietHD.IDHD = id;
                db.ChiTietHDs.Add(chiTietHD);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = id });
            }

            ViewBag.MaMatHang = new SelectList(db.MatHangs, "MaMatHang", "MaLoaiMH", chiTietHD.MaMatHang);
            ViewBag.IDHD = new SelectList(db.ThongTinHopDongs, "ID", "MaHD", chiTietHD.IDHD);
            return View(chiTietHD);
        }

        // GET: ChiTietHopDong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHD chiTietHD = db.ChiTietHDs.Find(id);
            if (chiTietHD == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMatHang = new SelectList(db.MatHangs, "MaMatHang", "MaLoaiMH", chiTietHD.MaMatHang);
            ViewBag.IDHD = new SelectList(db.ThongTinHopDongs, "ID", "MaHD", chiTietHD.IDHD);
            return View(chiTietHD);
        }

        // POST: ChiTietHopDong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDHD,MaMatHang,SoLuong,DonGia,Sum")] ChiTietHD chiTietHD)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["ID"]);
                db.Entry(chiTietHD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = id });
            }
            ViewBag.MaMatHang = new SelectList(db.MatHangs, "MaMatHang", "MaLoaiMH", chiTietHD.MaMatHang);
            ViewBag.IDHD = new SelectList(db.ThongTinHopDongs, "ID", "MaHD", chiTietHD.IDHD);
            return View(chiTietHD);
        }

        // GET: ChiTietHopDong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHD chiTietHD = db.ChiTietHDs.Find(id);
            if (chiTietHD == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHD);
        }

        // POST: ChiTietHopDong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int ID = Convert.ToInt32(Session["ID"]);
            ChiTietHD chiTietHD = db.ChiTietHDs.Find(id);
            db.ChiTietHDs.Remove(chiTietHD);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = ID });
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

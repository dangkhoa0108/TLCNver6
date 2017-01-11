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
    public class ChiTietPhieuNhapController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: ChiTietPhieuNhap
        public ActionResult Index()
        {
            var chiTietPNs = db.ChiTietPNs.Include(c => c.Kho).Include(c => c.MatHang).Include(c => c.ThongTinPN);
            return View(chiTietPNs.ToList());
        }

        // GET: ChiTietPhieuNhap/Details/5
        public ActionResult Details(int? ID)
        {
            Session["ID"] = ID.ToString();

            List<ChiTietPhieuNhapViewModel> model = new List<ChiTietPhieuNhapViewModel>();
            var join = (from ThongTinPN in db.ThongTinPNs
                        join
                        ChiTietPN in db.ChiTietPNs on ThongTinPN.ID equals ChiTietPN.IDPN
                        join MatHang in db.MatHangs on ChiTietPN.MaMatHang equals MatHang.MaMatHang
                        join Kho in db.Khoes on ChiTietPN.MaKho equals Kho.MaKho where (ThongTinPN.ID==ID)
                        select new
                        {
                            iD=ChiTietPN.ID,
                            idPN = ThongTinPN.MaPN,
                            tenMH = MatHang.TenMatHang,
                            tenKho = Kho.TenKho,
                            soLuong = ChiTietPN.SoLuong,
                            donGia = ChiTietPN.DonGia,
                            sum = ChiTietPN.Sum
                        }).ToList();
            foreach (var item in join)
            {
                model.Add(new ChiTietPhieuNhapViewModel()
                {
                    ID=item.iD,
                    MaPN = item.idPN,
                    TenMatHang = item.tenMH,
                    TenKho = item.tenKho,
                    SoLuong = item.soLuong,
                    DonGia = item.donGia,
                    Sum=item.sum
                });
            }
            return View(model);
        }

        // GET: ChiTietPhieuNhap/Create
        public ActionResult Create()
        {
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho");
            ViewBag.MaMatHang = new SelectList(db.MatHangs, "MaMatHang", "MaLoaiMH");
            ViewBag.IDPN = new SelectList(db.ThongTinPNs, "ID", "MaPN");
            return View();
        }

        // POST: ChiTietPhieuNhap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDPN,MaMatHang,MaKho,SoLuong,DonGia,Sum")] ChiTietPN chiTietPN)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["ID"]);
                chiTietPN.IDPN = id;
                db.ChiTietPNs.Add(chiTietPN);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = id });
            }

            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", chiTietPN.MaKho);
            ViewBag.MaMatHang = new SelectList(db.MatHangs, "MaMatHang", "MaLoaiMH", chiTietPN.MaMatHang);
            ViewBag.IDPN = new SelectList(db.ThongTinPNs, "ID", "MaPN", chiTietPN.IDPN);
            return View(chiTietPN);
        }

        // GET: ChiTietPhieuNhap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPN chiTietPN = db.ChiTietPNs.Find(id);
            if (chiTietPN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", chiTietPN.MaKho);
            ViewBag.MaMatHang = new SelectList(db.MatHangs, "MaMatHang", "MaLoaiMH", chiTietPN.MaMatHang);
            ViewBag.IDPN = new SelectList(db.ThongTinPNs, "ID", "MaPN", chiTietPN.IDPN);
            return View(chiTietPN);
        }

        // POST: ChiTietPhieuNhap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDPN,MaMatHang,MaKho,SoLuong,DonGia,Sum")] ChiTietPN chiTietPN)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["ID"]);
                db.Entry(chiTietPN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = id });
            }
            ViewBag.MaKho = new SelectList(db.Khoes, "MaKho", "TenKho", chiTietPN.MaKho);
            ViewBag.MaMatHang = new SelectList(db.MatHangs, "MaMatHang", "MaLoaiMH", chiTietPN.MaMatHang);
            ViewBag.IDPN = new SelectList(db.ThongTinPNs, "ID", "MaPN", chiTietPN.IDPN);
            return View(chiTietPN);
        }

        // GET: ChiTietPhieuNhap/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPN chiTietPN = db.ChiTietPNs.Find(id);
            if (chiTietPN == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPN);
        }

        // POST: ChiTietPhieuNhap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int ID = Convert.ToInt32(Session["ID"]);
            ChiTietPN chiTietPN = db.ChiTietPNs.Find(id);
            db.ChiTietPNs.Remove(chiTietPN);
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

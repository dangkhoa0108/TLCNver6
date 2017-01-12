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
    public class ChiTietPhieuXuatController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: ChiTietPhieuXuat
        public ActionResult Index()
        {
            var chiTietPXes = db.ChiTietPXes.Include(c => c.DonViGiaoNhan).Include(c => c.MatHang).Include(c => c.ThongTinPX);
            return View(chiTietPXes.ToList());
        }

       


        // GET: ChiTietPhieuXuat/Details/5
        public ActionResult Details(int? ID)
        {
            Session["ID"] = ID.ToString();
            List<ChiTietPhieuXuatViewModel> model = new List<ChiTietPhieuXuatViewModel>();
            var join = (from ThongTinPX in db.ThongTinPXes
                        join
                        ChiTietPX in db.ChiTietPXes on ThongTinPX.ID equals ChiTietPX.IDPX
                        join MatHang in db.MatHangs on ChiTietPX.MaMatHang equals MatHang.MaMatHang
                        join DonViGiaoNhan in db.DonViGiaoNhans on ChiTietPX.MaDV equals DonViGiaoNhan.MaDV
                        where (ThongTinPX.ID == ID)
                        select new
                        {
                            iD = ChiTietPX.ID,
                            idPX = ThongTinPX.MaPX,
                            tenMH = MatHang.TenMatHang,
                            tenDonVi = DonViGiaoNhan.TenDV,
                            soLuong = ChiTietPX.SoLuong,
                            donGia = ChiTietPX.DonGia,
                            sum = ChiTietPX.Sum
                        }).ToList();
            foreach (var item in join)
            {
                model.Add(new ChiTietPhieuXuatViewModel()
                {
                    ID = item.iD,
                    MaPX = item.idPX,
                    TenMatHang = item.tenMH,
                    TenDonVi = item.tenDonVi,
                    SoLuong = item.soLuong,
                    DonGia = item.donGia,
                    Sum = item.sum
                });
            }
            return View(model);
        }

        // GET: ChiTietPhieuXuat/Create
        public ActionResult Create()
        {
            ViewBag.MaDV = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV");
            ViewBag.MaMatHang = new SelectList(db.MatHangs, "MaMatHang", "TenMatHang");
            ViewBag.IDPX = new SelectList(db.ThongTinPXes, "ID", "MaPX");
            return View();
        }

        // POST: ChiTietPhieuXuat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDPX,MaMatHang,MaDV,SoLuong,DonGia,Sum")] ChiTietPX chiTietPX)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["ID"]);
                chiTietPX.IDPX = id;
                db.ChiTietPXes.Add(chiTietPX);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = id });
            }

            ViewBag.MaDV = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV", chiTietPX.MaDV);
            ViewBag.MaMatHang = new SelectList(db.MatHangs, "MaMatHang", "MaLoaiMH", chiTietPX.MaMatHang);
            ViewBag.IDPX = new SelectList(db.ThongTinPXes, "ID", "MaPX", chiTietPX.IDPX);
            return View(chiTietPX);
        }

        // GET: ChiTietPhieuXuat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPX chiTietPX = db.ChiTietPXes.Find(id);
            if (chiTietPX == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDV = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV", chiTietPX.MaDV);
            ViewBag.MaMatHang = new SelectList(db.MatHangs, "MaMatHang", "MaLoaiMH", chiTietPX.MaMatHang);
            ViewBag.IDPX = new SelectList(db.ThongTinPXes, "ID", "MaPX", chiTietPX.IDPX);
            return View(chiTietPX);
        }

        // POST: ChiTietPhieuXuat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDPX,MaMatHang,MaDV,SoLuong,DonGia,Sum")] ChiTietPX chiTietPX)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["ID"]);
                db.Entry(chiTietPX).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = id });
            }
            ViewBag.MaDV = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV", chiTietPX.MaDV);
            ViewBag.MaMatHang = new SelectList(db.MatHangs, "MaMatHang", "MaLoaiMH", chiTietPX.MaMatHang);
            ViewBag.IDPX = new SelectList(db.ThongTinPXes, "ID", "MaPX", chiTietPX.IDPX);
            return View(chiTietPX);
        }

        // GET: ChiTietPhieuXuat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPX chiTietPX = db.ChiTietPXes.Find(id);
            if (chiTietPX == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPX);
        }

        // POST: ChiTietPhieuXuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int ID = Convert.ToInt32(Session["ID"]);
            ChiTietPX chiTietPX = db.ChiTietPXes.Find(id);
            db.ChiTietPXes.Remove(chiTietPX);
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

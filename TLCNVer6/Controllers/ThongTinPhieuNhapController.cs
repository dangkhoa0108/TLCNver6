using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
            if (Session["Username"]!=null)
            {
                var thongTinPNs = db.ThongTinPNs.Include(t => t.DonViGiaoNhan).Include(t => t.Login);
                return View(thongTinPNs.ToList());
            }
            else
            {
                return Redirect("~/Login/Index");
            }
            
        }

        [HttpPost]
        public ActionResult Details(string DVGN, DateTime? NgayLap, DateTime? GiaTriDen)
        {
            List<BaoCaoChiTietPhieuNhapTheoThoiGian> model = new List<BaoCaoChiTietPhieuNhapTheoThoiGian>();
            var join = (from TTPN in db.ThongTinPNs
                        join DV in db.DonViGiaoNhans
                        on TTPN.MaDonVi equals DV.MaDV
                        join DN in db.Logins
                        on TTPN.NguoiLap equals DN.ID
                        where (TTPN.NgayLap >= NgayLap && TTPN.NgayLap <= GiaTriDen)
                        where (DV.MaDV == DVGN)
                        select new
                        {
                            maPN = TTPN.MaPN,
                            ngayLap = TTPN.NgayLap,
                            giaTriDen = TTPN.GiaTriDen,
                            nguoiLapPhieu = DN.HoTen,
                            tenDV = DV.TenDV,
                        }).ToList();
            foreach (var item in join)
            {
                model.Add(new BaoCaoChiTietPhieuNhapTheoThoiGian()
                {
                    MaPN = item.maPN,
                    NgayLap = item.ngayLap,
                    GiaTriDen = item.giaTriDen,
                    NguoiLap = item.nguoiLapPhieu,
                    TenDV = item.tenDV,
                });
            }
            return View(model);
        }
         
        public ActionResult Find()
        {
            ViewBag.DVGN = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV");
            return View();
        }


        public ActionResult LocTheoDonVi()
        {
            ViewBag.DonVi = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV");
            return View();
        }


        [HttpPost]
        public ActionResult Export(string DonVi)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/CrystalReportBaoCaoChiTietHangNhap.rpt")));
            var select = (from TTPN in db.ThongTinPNs
                          join DVGN in db.DonViGiaoNhans
                             on TTPN.MaDonVi equals DVGN.MaDV
                          join LG in db.Logins on TTPN.NguoiLap equals LG.ID
                          where TTPN.MaDonVi == DonVi
                          select new
                          {
                              MaPN = TTPN.MaPN ?? "No Value",
                              Ngaylap = TTPN.NgayLap.ToString() ?? "No Value",
                              GiaTriDen = TTPN.GiaTriDen.ToString() ?? "No Value",
                              NguoiLap = LG.HoTen ?? "No Value",
                              MaDonVI=DVGN.TenDV?? "No Value"
                          }).ToList();
            rd.SetDataSource(select);
            Response.Buffer = false;
            Response.ClearContent();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Báo cáo chi tiết phiếu nhập theo đơn vị.pdf");
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
            try
            {
                ThongTinPN thongTinPN = db.ThongTinPNs.Find(id);
                db.ThongTinPNs.Remove(thongTinPN);
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

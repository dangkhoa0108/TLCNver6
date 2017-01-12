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
    [PhanQuyen]
    public class ThongTinPhieuXuatController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: ThongTinPhieuXuat
        public ActionResult Index()
        {
            if(Session["Username"]!=null)
            {
                var thongTinPXes = db.ThongTinPXes.Include(t => t.Kho).Include(t => t.Login);
                return View(thongTinPXes.ToList());
            }
            else
            {
                return Redirect("~/Login/Index");
            }
            
        }

        public ActionResult LocTongGiaTriDaCap()
        {
            ViewBag.DV = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV");
            return View();
        }


        [HttpPost]
        public ActionResult ExportLTGT(string DV)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/CrystalReportBaoCaoTongGiaTriDaCap.rpt")));
            var select = (from TTPX in db.ThongTinPXes join CTPX in db.ChiTietPXes
                             on TTPX.ID equals CTPX.IDPX
                          join LG in db.Logins on TTPX.NguoiLap equals LG.ID
                          join MH in db.MatHangs on CTPX.MaMatHang equals MH.MaMatHang
                          join DVGN in db.DonViGiaoNhans on CTPX.MaDV equals DVGN.MaDV
                          where (CTPX.MaDV == DV /*&& TTPX.NgayLap >= NgayLap && TTPX.NgayLap <= GiaTriDen*/)
                          select new
                          {
                              MaPX = TTPX.MaPX ?? "No Value",
                              Ngaylap = TTPX.NgayLap.ToString() ?? "No Value",
                              GiaTriDen = TTPX.GiaTriDen.ToString() ?? "No Value",
                              NguoiLap = LG.HoTen ?? "No Value",
                              TenMatHang=MH.TenMatHang??"No Value",
                              SoLuong = CTPX.SoLuong ?? 0,
                              DonGia=CTPX.DonGia?? 0,
                              Sum=CTPX.Sum??0,
                          }).ToList();
            rd.SetDataSource(select);
            Response.Buffer = false;
            Response.ClearContent();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Báo cáo tổng giá trị đã cấp.pdf");
        }

        public ActionResult Find()
        {
            ViewBag.Kho = new SelectList(db.Khoes, "MaKho", "TenKho");
            return View();
        }


        public ActionResult LocTheoKho()
        {
            ViewBag.Kho = new SelectList(db.Khoes, "MaKho", "TenKho");
            return View();
        }

        [HttpPost]
        public ActionResult Export(string Kho)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/CrystalReportBaoCaoChiTietHangXuat.rpt")));
            var select = (from TTPX in db.ThongTinPXes
                          join K in db.Khoes
                             on TTPX.MaKho equals K.MaKho
                          join LG in db.Logins on TTPX.NguoiLap equals LG.ID
                          where TTPX.MaKho == Kho
                          select new
                          {
                              MaPX = TTPX.MaPX ?? "No Value",
                              Ngaylap = TTPX.NgayLap.ToString() ?? "No Value",
                              GiaTriDen = TTPX.GiaTriDen.ToString() ?? "No Value",
                              NguoiLap = LG.HoTen ?? "No Value",
                              MaKho = K.TenKho ?? "No Value"
                          }).ToList();
            rd.SetDataSource(select);
            Response.Buffer = false;
            Response.ClearContent();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Báo cáo chi tiết phiếu xuất theo kho.pdf");
        }




        [HttpPost]
        public ActionResult Details(string Kho, DateTime? NgayLap, DateTime? GiaTriDen)
        {
            List<BaoCaoChiTietPhieuXuatTheoThoiGian> model = new List<BaoCaoChiTietPhieuXuatTheoThoiGian>();
            var join = (from TTPX in db.ThongTinPXes
                        join K in db.Khoes
                        on TTPX.MaKho equals K.MaKho
                        join DN in db.Logins
                        on TTPX.NguoiLap equals DN.ID
                        where (TTPX.NgayLap >= NgayLap && TTPX.NgayLap <= GiaTriDen)
                        where (K.MaKho == Kho)
                        select new
                        {
                            maPX = TTPX.MaPX,
                            ngayLap = TTPX.NgayLap,
                            giaTriDen = TTPX.GiaTriDen,
                            nguoiLapPhieu = DN.HoTen,
                            tenKho = K.TenKho,
                        }).ToList();
            foreach (var item in join)
            {
                model.Add(new BaoCaoChiTietPhieuXuatTheoThoiGian()
                {
                    MaPX = item.maPX,
                    NgayLap = item.ngayLap,
                    GiaTriDen = item.giaTriDen,
                    NguoiLap = item.nguoiLapPhieu,
                    TenKho = item.tenKho,
                });
            }
            return View(model);
        }

        // GET: ThongTinPhieuXuat/Create
       

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
            try
            {
                ThongTinPX thongTinPX = db.ThongTinPXes.Find(id);
                db.ThongTinPXes.Remove(thongTinPX);
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

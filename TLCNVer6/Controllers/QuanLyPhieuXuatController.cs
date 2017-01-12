using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLCNVer6.Models;
using TLCNVer6.ViewModel;

namespace TLCNVer6.Controllers
{
    public class QuanLyPhieuXuatController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: QuanLyPhieuXuat
        public ActionResult Index()
        {
            if(Session["Username"]!=null)
            {
                ViewBag.DonViGN = new SelectList(db.DonViGiaoNhans, "MaDV", "TenDV");
                ViewBag.Kho = new SelectList(db.Khoes, "MaKho", "TenKho");
                ViewBag.MatHang = new SelectList(db.MatHangs, "MaMatHang", "TenMatHang");
                return View();
            }
            else
            {
                return Redirect("~/Login/Index");
            }
            
        }

        [HttpPost]
        public JsonResult SaveOrder(PhieuXuatViewModel O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (QuanLyKhoDuocPhamDbContext dc = new QuanLyKhoDuocPhamDbContext())
                {
                    ThongTinPX order = new ThongTinPX { MaPX = O.MaPX, NgayLap = O.NgayLap, GiaTriDen = O.GiaTriDen, NguoiLap = O.NguoiLap, MaKho = O.MaKho };
                    foreach (var i in O.ChiTietPX)
                    {
                        //
                        // i.TotalAmount = 
                        order.ChiTietPXes.Add(i);
                    }
                    order.NguoiLap = Session["IDU"].ToString();
                    dc.ThongTinPXes.Add(order);
                    dc.SaveChanges();
                    status = true;
                }
            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}

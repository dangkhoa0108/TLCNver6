using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLCNVer6.Models;
using TLCNVer6.ViewModel;

namespace TLCNVer6.Controllers
{
    public class QuanLyHopDongController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();
        // GET: QuanLyHopDong
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
        public JsonResult SaveOrder(HopDongViewModel O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (QuanLyKhoDuocPhamDbContext dc = new QuanLyKhoDuocPhamDbContext())
                {
                    ThongTinHopDong order = new ThongTinHopDong { MaHD = O.MaHD, TinhChat = O.TinhChat, MaKho = O.MaKho, MaDV = O.MaDV, NgayKi = O.NgayKi, NguoiLap=O.NguoiLap };
                    foreach (var i in O.ChiTietHD)
                    {
                        //
                        // i.TotalAmount = 
                        order.ChiTietHDs.Add(i);
                    }
                    order.NguoiLap = Session["IDU"].ToString();
                    dc.ThongTinHopDongs.Add(order);
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

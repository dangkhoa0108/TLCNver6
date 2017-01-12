using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TLCNVer6.Models
{
    public class PhanQuyen: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string session = HttpContext.Current.Session["Role"].ToString();

            if (session == "Admin")
            {
                //                          "controller-Action"
                string[] danhsachquyen =
                {
                    "ChiTietHopDong-Index","ChiTietHopDong-Details","ChiTietHopDong-Create", "ChiTietHopDong-Edit","ChiTietHopDong-Delete", "ChiTietHopDong-DeleteConfirmed", "ChiTietHopDong-Dispose",
                    "ChiTietPhieuNhap-Index","ChiTietPhieuNhap-Details","ChiTietPhieuNhap-Create", "ChiTietPhieuNhap-Edit", "ChiTietPhieuNhap-Delete", "ChiTietPhieuNhap-DeleteConfirmed", "ChiTietPhieuNhap-Dispose",
                    "ChiTietPhieuXuat-Index", "ChiTietPhieuXuat-Details", "ChiTietPhieuXuat-Create", "ChiTietPhieuXuat-Edit", "ChiTietPhieuXuat-Delete","ChiTietPhieuXuat-DeleteConfirmed", "ChiTietPhieuXuat-Dispose",
                    "QuanLyDVGNhan-Index","QuanLyDVGNhan-Details","QuanLyDVGNhan-Create","QuanLyDVGNhan-Edit", "QuanLyDVGNhan-Delete","QuanLyDVGNhan-DeleteConfirmed","QuanLyDVGNhan-Dispose",
                    "QuanLyHopDong-Index", "QuanLyHopDong-SaveOrder",
                    "QuanLyKho-Index","QuanLyKho-Find","QuanLyKho-Details", "QuanLyKho-Create", "QuanLyKho-Edit", "QuanLyKho-Delete", "QuanLyKho-DeleteConfirmed", "QuanLyKho-Dispose",
                     "QuanLyLoaiKho-Index","QuanLyLoaiKho-Details","QuanLyLoaiKho-Create","QuanLyLoaiKho-Edit", "QuanLyLoaiKho-Delete","QuanLyLoaiKho-DeleteConfirmed","QuanLyLoaiKho-Dispose",
                      "QuanLyLoaiMatHang-Index","QuanLyLoaiMatHang-Details","QuanLyLoaiMatHang-Create","QuanLyLoaiMatHang-Edit", "QuanLyLoaiMatHang-Delete","QuanLyLoaiMatHang-DeleteConfirmed","QuanLyLoaiMatHang-Dispose",
                      "QuanLyMatHang-Index","QuanLyMatHang-Find","QuanLyMatHang-Details", "QuanLyMatHang-Create", "QuanLyMatHang-Edit", "QuanLyMatHang-Delete", "QuanLyMatHang-DeleteConfirmed", "QuanLyMatHang-Dispose",
                      "QuanLyPhieuNhap-Index", "QuanLyPhieuNhap-SaveOrder",
                      "QuanLyPhieuXuat-Index", "QuanLyPhieuXuat-SaveOrder",
                       "ThongTinHopDong-Index","ThongTinHopDong-Details","ThongTinHopDong-Create","ThongTinHopDong-Edit", "ThongTinHopDong-Delete","ThongTinHopDong-DeleteConfirmed","ThongTinHopDong-Dispose",
                       "ThongTinPhieuNhap-Index","ThongTinPhieuNhap-Details","ThongTinPhieuNhap-Find", "ThongTinPhieuNhap-LocTheoDonVi","ThongTinPhieuNhap-Export","ThongTinPhieuNhap-Edit","ThongTinPhieuNhap-Delete","ThongTinPhieuNhap-DeleteConfirmed", "ThongTinPhieuNhap-Dispose",
                       "ThongTinPhieuXuat-Index","ThongTinPhieuXuat-LocTongGiaTriDaCap","ThongTinPhieuXuat-ExportLTGT", "ThongTinPhieuXuat-Find","ThongTinPhieuXuat-LocTheoKho","ThongTinPhieuXuat-Export","ThongTinPhieuXuat-Details","ThongTinPhieuXuat-Edit", "ThongTinPhieuXuat-Delete","ThongTinPhieuXuat-DeleteConfirmed", "ThongTinPhieuXuat-Dispose"
                };
                string actionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName
                + "-" + filterContext.ActionDescriptor.ActionName;
                if (!danhsachquyen.Contains(actionName))
                {
                    filterContext.Result = new RedirectResult("~/Login/Error");
                }
            }

            else if (session == "Ban Tài Chính")
            {
                string[] danhsachquyen =
                    {
                    "ChiTietHopDong-Index","ChiTietHopDong-Details","ChiTietHopDong-DeleteConfirmed", "ChiTietHopDong-Dispose",
                    "ChiTietPhieuNhap-Index","ChiTietPhieuNhap-Details", "ChiTietPhieuNhap-DeleteConfirmed", "ChiTietPhieuNhap-Dispose",
                    "ChiTietPhieuXuat-Index", "ChiTietPhieuXuat-Details","ChiTietPhieuXuat-DeleteConfirmed", "ChiTietPhieuXuat-Dispose",
                    "QuanLyDVGNhan-Index","QuanLyDVGNhan-Details","QuanLyDVGNhan-DeleteConfirmed","QuanLyDVGNhan-Dispose",
                    "QuanLyKho-Index","QuanLyKho-Find","QuanLyKho-Details","QuanLyKho-DeleteConfirmed", "QuanLyKho-Dispose",
                     "QuanLyLoaiKho-Index","QuanLyLoaiKho-Details","QuanLyLoaiKho-DeleteConfirmed","QuanLyLoaiKho-Dispose",
                      "QuanLyLoaiMatHang-Index","QuanLyLoaiMatHang-Details","QuanLyLoaiMatHang-DeleteConfirmed","QuanLyLoaiMatHang-Dispose",
                      "QuanLyMatHang-Index","QuanLyMatHang-Find","QuanLyMatHang-Details", "QuanLyMatHang-DeleteConfirmed", "QuanLyMatHang-Dispose",
                       "ThongTinHopDong-Index","ThongTinHopDong-Details","ThongTinHopDong-Dispose",
                       "ThongTinPhieuNhap-Index","ThongTinPhieuNhap-Details","ThongTinPhieuNhap-Find", "ThongTinPhieuNhap-LocTheoDonVi","ThongTinPhieuNhap-Export","ThongTinPhieuNhap-DeleteConfirmed", "ThongTinPhieuNhap-Dispose",
                       "ThongTinPhieuXuat-Index","ThongTinPhieuXuat-LocTongGiaTriDaCap","ThongTinPhieuXuat-ExportLTGT", "ThongTinPhieuXuat-Find","ThongTinPhieuXuat-LocTheoKho","ThongTinPhieuXuat-Export","ThongTinPhieuXuat-Details","ThongTinPhieuXuat-DeleteConfirmed", "ThongTinPhieuXuat-Dispose"
                };
                string actionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName
                + "-" + filterContext.ActionDescriptor.ActionName;
                if (!danhsachquyen.Contains(actionName))
                {
                    filterContext.Result = new RedirectResult("~/Login/Error");
                }
            }

            else if (session == "Ban Kế Hoạch")
            {
                string[] danhsachquyen = {
                "ChiTietHopDong-Index","ChiTietHopDong-Details","ChiTietHopDong-Create", "ChiTietHopDong-Edit","ChiTietHopDong-Delete", "ChiTietHopDong-DeleteConfirmed", "ChiTietHopDong-Dispose",
                    "ChiTietPhieuNhap-Index","ChiTietPhieuNhap-Details","ChiTietPhieuNhap-Create", "ChiTietPhieuNhap-Edit", "ChiTietPhieuNhap-Delete", "ChiTietPhieuNhap-DeleteConfirmed", "ChiTietPhieuNhap-Dispose",
                    "ChiTietPhieuXuat-Index", "ChiTietPhieuXuat-Details", "ChiTietPhieuXuat-Create", "ChiTietPhieuXuat-Edit", "ChiTietPhieuXuat-Delete","ChiTietPhieuXuat-DeleteConfirmed", "ChiTietPhieuXuat-Dispose",
                    "QuanLyDVGNhan-Index","QuanLyDVGNhan-Details","QuanLyDVGNhan-Create","QuanLyDVGNhan-Edit", "QuanLyDVGNhan-Delete","QuanLyDVGNhan-DeleteConfirmed","QuanLyDVGNhan-Dispose",
                    "QuanLyHopDong-Index", "QuanLyHopDong-SaveOrder",
                    "QuanLyKho-Index","QuanLyKho-Details", "QuanLyKho-Create", "QuanLyKho-Edit", "QuanLyKho-Delete", "QuanLyKho-DeleteConfirmed", "QuanLyKho-Dispose",
                     "QuanLyLoaiKho-Index","QuanLyLoaiKho-Details","QuanLyLoaiKho-Create","QuanLyLoaiKho-Edit", "QuanLyLoaiKho-Delete","QuanLyLoaiKho-DeleteConfirmed","QuanLyLoaiKho-Dispose",
                      "QuanLyLoaiMatHang-Index","QuanLyLoaiMatHang-Details","QuanLyLoaiMatHang-Create","QuanLyLoaiMatHang-Edit", "QuanLyLoaiMatHang-Delete","QuanLyLoaiMatHang-DeleteConfirmed","QuanLyLoaiMatHang-Dispose",
                      "QuanLyMatHang-Index","QuanLyMatHang-Details", "QuanLyMatHang-Create", "QuanLyMatHang-Edit", "QuanLyMatHang-Delete", "QuanLyMatHang-DeleteConfirmed", "QuanLyMatHang-Dispose",
                      "QuanLyPhieuNhap-Index", "QuanLyPhieuNhap-SaveOrder",
                      "QuanLyPhieuXuat-Index", "QuanLyPhieuXuat-SaveOrder",
                       "ThongTinHopDong-Index","ThongTinHopDong-Details","ThongTinHopDong-Create","ThongTinHopDong-Edit", "ThongTinHopDong-Delete","ThongTinHopDong-DeleteConfirmed","ThongTinHopDong-Dispose",
                       "ThongTinPhieuNhap-Index","ThongTinPhieuNhap-Details","ThongTinPhieuNhap-Export","ThongTinPhieuNhap-Edit","ThongTinPhieuNhap-Delete","ThongTinPhieuNhap-DeleteConfirmed", "ThongTinPhieuNhap-Dispose",
                       "ThongTinPhieuXuat-Index","ThongTinPhieuXuat-ExportLTGT", "ThongTinPhieuXuat-Export","ThongTinPhieuXuat-Details","ThongTinPhieuXuat-Edit", "ThongTinPhieuXuat-Delete","ThongTinPhieuXuat-DeleteConfirmed", "ThongTinPhieuXuat-Dispose"
                };
                string actionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName
                + "-" + filterContext.ActionDescriptor.ActionName;
                if (!danhsachquyen.Contains(actionName))
                {
                    filterContext.Result = new RedirectResult("~/Login/Error");
                }
            }

        }
    }
}
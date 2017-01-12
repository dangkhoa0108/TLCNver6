using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLCNVer6.Models;

namespace TLCNVer6.Controllers
{
    public class UserProfileController : Controller
    {
        QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();
        // GET: UserProfile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            string role = Session["Role"].ToString();
            if (role == "Admin")
            {
                return Redirect("~/Login/Index");
            }
            else if (role == "Ban Kế Hoạch")
            {
                return Redirect("~/Login/Index_KeHoach");
            }
            else if (role == "Ban Tài Chính")
            {
                return Redirect("~/Login/Index_TaiChinh");
            }
            else
            {
                ModelState.AddModelError("", "Err");
                // RedirectToAction("Login", "AdminLogin");
            }
            return View();
        }

        public ActionResult CapNhatPass()
        {
            string userID = (Session["IDU"].ToString());
            Login user = db.Logins.SingleOrDefault(x => x.ID == userID);

            string PassOld = Request.Form["txtPassOld"].ToString();
            string Pass = Request.Form["txtPass"].ToString();
            string RePass = Request.Form["txtRePass"].ToString();
            if (PassOld == user.Password && Pass == RePass)
            {
                user.Password = Pass;
                db.SaveChanges();
                return Redirect("~/UserProfile/Index");
            }

            return Redirect("~/UserProfile/signError");
        }
        public ActionResult signError()
        {
            return View();
        }
        public ActionResult CapNhatPhone()
        {
            string userID = (Session["IDU"].ToString());
            Login user = db.Logins.SingleOrDefault(x => x.ID == userID);
            string phone = Request.Form["txtPhone"].ToString();
            user.SoDT = phone;
            Session["SoDT"] = user.SoDT;
            db.SaveChanges();

            return Redirect("~/UserProfile/Index");

        }
        public ActionResult CapNhatName()
        {
            string userID = (Session["IDU"].ToString());
            Login user = db.Logins.SingleOrDefault(x => x.ID == userID);
            string name = Request.Form["nickname_new"].ToString();
            user.HoTen = name;
            Session["HoTen"] = user.HoTen;
            db.SaveChanges();

            return Redirect("~/UserProfile/Index");

        }

        public ActionResult CapNhatDiaChi()
        {
            string userID = (Session["IDU"].ToString());
            Login user = db.Logins.SingleOrDefault(x => x.ID == userID);
            string diachi = Request.Form["address_new"].ToString();
            user.DiaChi = diachi;
            Session["DiaChi"] = user.DiaChi;
            db.SaveChanges();

            return Redirect("~/UserProfile/Index");

        }
    }
}
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
    public class LoginController : Controller
    {
        private QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext();

        // GET: Login
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                return View(db.Logins.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Login()
        {
            ViewBag.Role = new SelectList(db.Logins, "Role", "Role");
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult DelError()
        {
            return View();
        }

        public ActionResult CheckLogin()
        {
            if (Session["Role"].ToString() == "Admin")
            {
                return RedirectToAction("Index");
            }
            else if (Session["Role"].ToString() == "Ban Kế Hoạch")
            {
                return RedirectToAction("Index_KeHoach");
            }
            else if (Session["Role"].ToString() == "Ban Tài Chính")
            {
                return RedirectToAction("Index_TaiChinh");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult Login(Login user)
        {
            using (QuanLyKhoDuocPhamDbContext db = new QuanLyKhoDuocPhamDbContext())
            {
                var usr = db.Logins.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password && u.Role == user.Role);
                if (usr != null)
                {
                    Session["IDU"] = usr.ID.ToString();
                    Session["Username"] = usr.Username.ToString();
                    Session["HoTen"] = usr.HoTen.ToString();
                    Session["DiaChi"] = usr.DiaChi.ToString();
                    Session["Password"] = usr.Password.ToString();
                    Session["SoDT"] = usr.SoDT.ToString();
                    Session["Role"] = usr.Role.ToString();

                    if (Session["Role"].ToString() == "Admin")
                    {
                        return RedirectToAction("Index");
                    }
                    else if (Session["Role"].ToString() == "Ban Kế Hoạch")
                    {
                        return RedirectToAction("Index_KeHoach");
                    }
                    else if (Session["Role"].ToString() == "Ban Tài Chính")
                    {
                        return RedirectToAction("Index_TaiChinh");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu sai");
                }


            }
            return View();
        }

        public ActionResult Index_KeHoach()
        {
            if (Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        public ActionResult Index_TaiChinh()
        {
            if (Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,Password,Role,HoTen,DiaChi,SoDT")] Login login)
        {
            if (ModelState.IsValid)
            {
                db.Logins.Add(login);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(login);
        }

        // GET: Login/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,Role,HoTen,DiaChi,SoDT")] Login login)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(login);
        }

        // GET: Login/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Login login = db.Logins.Find(id);
                db.Logins.Remove(login);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return RedirectToAction("DelError");
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

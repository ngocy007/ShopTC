using ShopTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopTC.Controllers
{
    public class DangNhapController : Controller
    {
        private QLBHTCEntities db = new QLBHTCEntities();
        public bool CheckUser(string username, string password)
        {
            var kq = db.Users.Where(x => x.Email == username && x.MatKhau == password).ToList();

            if (kq.Count() > 0)
            {
                Session["HoTen"] = kq.First().TenUser;
                Session["ID"] = kq.First().MaUser;
                if (kq.First().admin == true)
                {
                    Session["Admin"] = kq.First().admin;
                }
                return true;
            }
            else
            {
                Session["HoTen"] = null;
                return false;
            }
        }
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string Email, string password)
        {
            if (ModelState.IsValid)
            {
                if (CheckUser(Email, password))
                {
                    FormsAuthentication.SetAuthCookie(Email, true);
                    return RedirectToAction("Index", "ThuCungs");
                }
                else
                    ModelState.AddModelError("", "Tên đăng nhập hoặc tài khoản không đúng.");
            }
            var u = new Users();
            u.MatKhau = password;
            u.Email = Email;
            return View(u);
        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            FormsAuthentication.SignOut();
            return RedirectToAction("DangNhap", "DangNhap");
        }
        // GET: DangNhap
        public ActionResult Index()
        {
            return View();
        }

        // GET: DangNhap/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DangNhap/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DangNhap/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DangNhap/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DangNhap/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DangNhap/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DangNhap/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

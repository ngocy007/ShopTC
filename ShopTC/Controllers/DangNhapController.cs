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
                    return RedirectToAction("Index2", "ThuCungs");
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

            //Session.Clear();//remove session

            Session.Remove("HoTen");
            Session.Remove("ID");
            Session.Remove("Admin");
            FormsAuthentication.SignOut();
            return RedirectToAction("DangNhap", "DangNhap");
        }
        // GET: DangNhap
        public ActionResult Index()
        {
            return View();
        }



    }
}

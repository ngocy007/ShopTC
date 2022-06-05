using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopTC.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        private const string CartSession = "CartSesion";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCart(string id)
        {
            Session[CartSession] = "alo";

            if (Session[CartSession] == null)
            {

            }
            else
            {

            }

            return View();
        }
    }
}
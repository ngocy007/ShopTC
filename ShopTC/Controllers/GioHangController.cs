using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopTC.Controllers;
using ShopTC.Models;

namespace ShopTC.Controllers
{
    public class GioHangController : Controller
    {
        private QLBHTCEntities db = new QLBHTCEntities();
        // GET: GioHang
        private const string CartSession = "CartSesion";
        public ActionResult Index()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "DangNhap");

            }

            var cart = Session[CartSession];
            var list = new List<Cart>();
            if (cart != null)
            {
                list = (List<Cart>)cart;
            }

            var id = Session["ID"];
            var temp = db.Users.Find(id);

            if (temp != null)
            {
                ViewBag.name = temp.TenUser;
                ViewBag.address = temp.DiaChi;
                ViewBag.phone = temp.SDT;
                ViewBag.email = temp.Email;
            }

            return View(list);
        }

        public ActionResult AddToCart(string id)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "DangNhap");

            }


            var cart = Session[CartSession];

            var temp = db.ThuCung.Find(id);

            if (cart != null)
            {
                // lấy giá trị của sesion
                List<Cart> list = (List<Cart>)cart;


                // tìm kiếm
                if (list.Exists(x => x.id == id))
                {
                    // tăng lên 1 đơn vị
                    foreach (var item in list)
                    {
                        if (item.id == id)
                        {
                            item.quantity++;
                        }
                    }
                }
                else
                {

                    // nếu tìm k ra thì add mới vào
                    Cart c = new Cart(temp.MaTC, temp.TenTC, temp.HinhAnhMH, temp.GiaTC, 1);
                    list.Add(c);
                }
                Session[CartSession] = list;
            }
            else
            {

                // chưa có thì tạo ra giỏ hàng 
                Cart c = new Cart(temp.MaTC, temp.TenTC, temp.HinhAnhMH, temp.GiaTC, 1);
                List<Cart> list = new List<Cart>();

                // thêm hàng zô
                list.Add(c);

                // gán lại sesion
                Session[CartSession] = list;

            }

            return RedirectToAction("Index2", "ThuCungs", db.ThuCung);
        }

        public ActionResult addQuantity(string id, string a)
        {

            var cart = Session[CartSession];

            // lấy giá trị của sesion
            List<Cart> list = (List<Cart>)cart;


            // tìm kiếm
            if (list.Exists(x => x.id == id))
            {
                // tăng lên 1 đơn vị
                foreach (var item in list)
                {
                    if (item.id == id)
                    {
                        item.quantity = a == "+" ? item.quantity + 1 : item.quantity - 1;
                    }
                    if (item.quantity == 0)
                    {
                        list.Remove(item);
                        return RedirectToAction("Index", list);
                    }
                }
            }
            Session[CartSession] = list;


            return RedirectToAction("Index", list);
        }

        public ActionResult removeEach(string id)
        {
            var cart = Session[CartSession];

            // lấy giá trị của sesion
            List<Cart> list = (List<Cart>)cart;


            if (list.Exists(x => x.id == id))
            {
                // tăng lên 1 đơn vị
                foreach (var item in list)
                {
                    if (item.id == id)
                    {
                        list.Remove(item);
                        return RedirectToAction("Index", list);
                    }
                }
            }
            Session[CartSession] = list;


            return RedirectToAction("Index", list);
        }

        public ActionResult RemoveAll()
        {
            Session.Remove(CartSession);
            var cart = Session[CartSession];

            List<Cart> list = (List<Cart>)cart;

            return RedirectToAction("Index", list);
        }

        [HttpGet]
        public ActionResult Pay(string name, string address, string phone, string mail, string sale)
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "DangNhap");

            }

            var cart = Session[CartSession];
            var list = new List<Cart>();
            if (cart != null)
            {
                // tổng tiền
                int sum = 0;

                // lấy sesion
                list = (List<Cart>)cart;


                // thêm Hóa Đơn
                HoaDon hoaDon = new HoaDon();
                Random rnd = new Random();
                hoaDon.MaHD = DateTime.Now.ToString("ddMM") + rnd.Next(1, 999999);
                hoaDon.MaUser = Session["ID"].ToString();
                hoaDon.NgayTao = DateTime.Now;
                hoaDon.TenNguoiShip = name;
                hoaDon.DiaChiShip = address;
                hoaDon.SDTShip = phone;
                hoaDon.EmailShip = mail;
                foreach (var item in list)
                {
                    sum += item.price * item.quantity;
                }
                hoaDon.TongTien = sum;


                db.HoaDon.Add(hoaDon);
                db.SaveChanges();
                // thêm chi tiết hóa đơn
                foreach (var item in list)
                {
                    var cthd = new CTHoaDon();
                    cthd.MaHD = hoaDon.MaHD;
                    cthd.MaTC = item.id;
                    cthd.SoLuong = item.quantity;
                    cthd.DonGia = item.price;
                    db.CTHoaDon.Add(cthd);

                }

                db.SaveChanges();
                return View();


            }

            return View(db.HoaDon.ToList());
        }

    }
}
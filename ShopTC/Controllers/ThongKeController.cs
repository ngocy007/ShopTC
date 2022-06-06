using ShopTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopTC.Controllers
{
    public class ThongKeController : Controller
    {
        private QLBHTCEntities db = new QLBHTCEntities();
        // GET: ThongKe

        public ActionResult Index()
        {

            return View(db.HoaDon.ToList());
        }

        [HttpPost]

        public ActionResult Index(string nameStart, string nameEnd)
        {

            DateTime start = DateTime.Parse(nameStart);
            DateTime end = DateTime.Parse(nameEnd);
            var hd = db.HoaDon.Where(x => x.NgayTao >= start && x.NgayTao <= end);

            return View(hd.ToList());
        }



        // GET: ThongKe/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ThongKe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ThongKe/Create
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

        // GET: ThongKe/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ThongKe/Edit/5
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

        // GET: ThongKe/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ThongKe/Delete/5
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

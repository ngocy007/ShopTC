using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopTC.Models;
using System.Linq;

namespace ShopTC.Controllers
{
    public class ThuCungsController : Controller
    {
        private QLBHTCEntities db = new QLBHTCEntities();
        string LayMaTC()
        {
            var maMax = db.ThuCung.ToList().Select(n => n.MaTC).Max();
            int maTC = int.Parse(maMax.Substring(2)) + 1;
            string NV = String.Concat("000", maTC.ToString());
            return "TC" + NV.Substring(maTC.ToString().Length - 1);
        }


        // GET: ThuCungs
        public ActionResult Index(String Ten = "")
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                return RedirectToAction("Index2", "ThuCungs");
            }
            else
            {
                var thuCungs = db.ThuCung.Include(t => t.LoaiThuCung);
                ViewBag.MaLoai = new SelectList(db.LoaiThuCung, "MaLoai", "TenLoai");
                return View(thuCungs.ToList());
                
            }
        }
        [HttpGet]
        public ActionResult TimKiem(string Ten)
        {
            ViewBag.Ten = Ten;
            var TC = db.ThuCung.Where(t => t.TenTC == Ten);
            if (TC.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(TC.ToList());
        }
        public ActionResult Index2()
        {
            return View(db.ThuCung.ToList());
        }

        // GET: ThuCungs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuCung thuCung = db.ThuCung.Find(id);
            if (thuCung == null)
            {
                return HttpNotFound();
            }
            return View(thuCung);
        }
        
        // GET: ThuCungs/Create
        public ActionResult Create()
        {
            ViewBag.MaTC = LayMaTC();
            ViewBag.MaLoai = new SelectList(db.LoaiThuCung, "MaLoai", "TenLoai");
            ViewBag.MaCC = new SelectList(db.NHACC, "MaCC", "TenCC");
            return View();
        }

        // POST: ThuCungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTC,TenTC,GiaTC,SoLuong,GT,MoTa,HinhAnhMH,MaLoai,MaCC,NgaySinh")] ThuCung thuCung)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgTC = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgTC.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/" + postedFileName);
            imgTC.SaveAs(path);
            if (ModelState.IsValid)
            {
                thuCung.MaTC = LayMaTC();
                thuCung.HinhAnhMH = postedFileName;
                db.ThuCung.Add(thuCung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.LoaiThuCung, "MaLoai", "TenLoai", thuCung.MaLoai);
            ViewBag.MaCC = new SelectList(db.NHACC, "MaCC", "TenCC", thuCung.MaCC);
            return View(thuCung);
        }

        // GET: ThuCungs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuCung thuCung = db.ThuCung.Find(id);
            if (thuCung == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LoaiThuCung, "MaLoai", "TenLoai", thuCung.MaLoai);
            ViewBag.MaCC = new SelectList(db.NHACC, "MaCC", "TenCC", thuCung.MaCC);
            return View(thuCung);
        }

        // POST: ThuCungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTC,TenTC,GiaTC,SoLuong,GT,MoTa,HinhAnhMH,MaLoai,MaCC,NgaySinh")] ThuCung thuCung)
        {
            var imgTC = Request.Files["Avatar"];
            try
            {
                //Lấy thông tin từ input type=file có tên Avatar
                string postedFileName = System.IO.Path.GetFileName(imgTC.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Images/" + postedFileName);
                imgTC.SaveAs(path);
            }
            catch
            { }
            if (ModelState.IsValid)
            {
                db.Entry(thuCung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiThuCung, "MaLoai", "TenLoai", thuCung.MaLoai);
            ViewBag.MaCC = new SelectList(db.NHACC, "MaCC", "TenCC", thuCung.MaCC);
            return View(thuCung);
        }

        // GET: ThuCungs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuCung thuCung = db.ThuCung.Find(id);
            if (thuCung == null)
            {
                return HttpNotFound();
            }
            return View(thuCung);
        }

        // POST: ThuCungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ThuCung thuCung = db.ThuCung.Find(id);
            db.ThuCung.Remove(thuCung);
            db.SaveChanges();
            return RedirectToAction("Index"); ;
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

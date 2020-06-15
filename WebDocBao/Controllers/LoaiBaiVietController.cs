using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocBao.Models;
using System.Data.Entity;
using System.Windows.Documents;

namespace WebDocBao.Controllers
{
    public class LoaiBaiVietController : Controller
    {
        // GET: LoaibaiViet
        DocBaoEntitles1 db = new DocBaoEntities1();

        public ParatialViewResult LoaiBaiViet()
        {
            List < LoaiBaiViet > 1 = db.LoaiBaiViets.ToList<LoaiBaiViet>();
            return ParatialView(1);
        }

        private object GetLoaiBaiViets()
        {
            return db.LoaiBaiViets;
        }

        public ParatialViewResult BaiVietMoi()
        {
            List <LoaiBaiViet> l = db.LoaiBaiViets.ToList<LoaiBaiViet>();
            return ParatialView(1);
        }

        private ParatialViewResult ParatialView(int v)
        {
            throw new NotImplementedException();
        }

        public ViewResult BaiVietTheoLoai(String Maloai="01")
        {

            LoaiBaiViet 1 = db.LoaiBaiViets.SingleOrDefault(n => n.Maloai == Maloai);
            {
                Response.StatusCode = 404;
                return null;
            }
        }


        //[Authorize(Users = "admin")]
        public ActionResult()
        {
            List<LoaiBaiViet> lst = db.LoaiBaiViets.ToList<LoaiBaiViet>();
            return View(lst);
        }
        public ActionResult Them()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Them([Bind(Include = "Maloai, Tenloai")] LoaiBaiViet loai)
        {
            try
            {

                db.LoaiBaiViets.Add(loai);
                db.SaveChanges();
                return RedirectToAction("QLLoaiBaiViet");

            }
            catch 
            { 
                return View(); 
            }
        }
        public ActionResult Sua(string id)
        {
            var loai = db.LoaiBaiViets.Select(p => p).Where(p => p.maloai == id).FirstOrDefault();
            return View(loai);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sua(LoaiBaiViet editLoai)
        {

            try
            {

                var l = db.LoaiBaiViets.Select(p => p).Where(p => p.Maloai == editLoai.maLoai).FirstOrDefault();
                l.tenLoai = editLoai.tenLoai;
                db.SaveChange();
                return RedirectToAction("QLLoaiBaiViet");

            }
            catch
            {

                return View();

            }

        }
        public ActionResult Xoa(string id)
        {
            var loai = db.LoaiBaiViets.Select(p => p).Where(p => p.Maloai == id).FirstOrDefault();
            return View(loai);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Xoa(string id, FormCollection f)
        {

            try
            {

                var l = db.LoaiBaiViets.Select(p => p).Where(p => p.Maloai == id).FirstOrDefault();
                db.Loaibaiviets.Remove(l);
                db.SaveChanges();
                return RedirectToAction("QLLoaiBaiViet");

            }
            catch
            {

                return View();

            }

        }
    
    }
}

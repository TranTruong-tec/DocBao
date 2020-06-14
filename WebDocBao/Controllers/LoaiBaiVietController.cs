using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocBao.Models;
using System.Data.Entity;

namespace WebDocBao.Controllers
{
    public class LoaiBaiVietController : Controller
    {
        // GET: LoaibaiViet
        DocBaoEntities1 db = new DocBaoEntities1();

        public PartialViewResult LoaiBaiViet()
        {
            List<LoaiBaiViet> l = db.LoaiBaiViets.ToList<LoaiBaiViet>();
            return PartialView(l);
        }


        public PartialViewResult BaiVietMoi()
        {
            List<LoaiBaiViet> l = db.LoaiBaiViets.ToList<LoaiBaiViet>();
            return PartialView(l);
        }

        public ViewResult BaiVietTheoLoai(string maLoai = "01")
        {

            LoaiBaiViet l = db.LoaiBaiViets.SingleOrDefault(n => n.maLoai == maLoai);
            if (l == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<BaiViet> lstBV = db.BaiViets.Where(n => n.maLoai == maLoai).OrderByDescending(n => n.ngayDang).ToList();
            if (lstBV.Count == 0)
            {
                ViewBag.BaiViet = "Không có bài viết nào thuộc chủ đề " + l.tenLoai;
            }

            ViewBag.lstLoaiBV = db.LoaiBaiViets.ToList();
            return View(lstBV);
        }
        //[Authorize(Users = "admin")]
        public ActionResult QLLoaiBaiViet()
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
        public ActionResult Them([Bind(Include = "maLoai, tenLoai")] LoaiBaiViet loai)
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
            var loai = db.LoaiBaiViets.Select(p => p).Where(p => p.maLoai == id).FirstOrDefault();
            return View(loai);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sua(LoaiBaiViet editLoai)
        {

            try
            {

                var l = db.LoaiBaiViets.Select(p => p).Where(p => p.maLoai == editLoai.maLoai).FirstOrDefault();
                l.tenLoai = editLoai.tenLoai;
                db.SaveChanges();
                return RedirectToAction("QLLoaiBaiViet");

            }
            catch
            {

                return View();

            }

        }
        public ActionResult Xoa(string id)
        {
            var loai = db.LoaiBaiViets.Select(p => p).Where(p => p.maLoai == id).FirstOrDefault();
            return View(loai);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Xoa(string id, FormCollection f)
        {

            try
            {

                var l = db.LoaiBaiViets.Select(p => p).Where(p => p.maLoai == id).FirstOrDefault();
                db.LoaiBaiViets.Remove(l);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocBao.Models;
using System.IO;
using WebDocBao.CheckSession;

namespace WebDocBao.Controllers
{
    public class BaiVietController : Controller
    {
        // GET: BaiViet
        private DocBaoEntities1 db = new DocBaoEntities1();

        
        public ActionResult XemChiTiet(string maBV)
        {
            BaiViet bv = db.BaiViets.SingleOrDefault(n => n.maBaiViet == maBV);
            if (bv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.TenLoaiBV = db.LoaiBaiViets.Single(n => n.maLoai == bv.maLoai).tenLoai;
            ViewBag.DSComment = bv.Comments.OrderByDescending(x => x.ngayComment).ToList<Comment>();
            ViewBag.BaiViet = bv;
            Session["maBaiViet"] = bv.maBaiViet;
            return View();
        }
        [SessionTimeout]
        [HttpGet]

        public ActionResult NhapBaiViet()
        {
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "maLoai", "tenLoai");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NhapBaiViet(BaiViet bv, HttpPostedFileBase myfileImage)
        {
            string don = DateTime.Now.ToString("yyyyMMddhhmmss");
            bv.maBaiViet = "BV_" + don;
            bv.ngayDang = DateTime.Now;
            if (ModelState.IsValid)
            {

                db.BaiViets.Add(bv);

                var getlas = from baiviet in db.BaiViets
                             select baiviet;

                BaiViet lbv = (BaiViet)getlas.First();
                string fileName = don + "_" + myfileImage.FileName.Trim();
                bv.hinhAnh = fileName;
                var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                myfileImage.SaveAs(path);
                db.SaveChanges();
                return RedirectToAction("QLBaiViet");
            }
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "maLoai", "tenLoai", bv.maLoai);
            return View(bv);
        }
        public ActionResult Sua(string id)
        {
            BaiViet bv = db.BaiViets.SingleOrDefault(n => n.maBaiViet == id);
            
            if (bv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "maLoai", "tenLoai", bv.maLoai);
            return View(bv);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sua(BaiViet bv, HttpPostedFileBase myfileImage)
        {
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "maLoai", "tenLoai");
            if(myfileImage==null)
            {
                ViewBag.ThongBao = "Vui lòng chọn Ảnh Bìa";
                return View();
            }
            else
            {
                if(ModelState.IsValid)
                {
                    var filename = Path.GetFileName(myfileImage.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    if (System.IO.File.Exists(path))
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    else
                    {
                        myfileImage.SaveAs(path);
                    }
                    bv.hinhAnh = filename;
                    UpdateModel(bv);
                    db.SaveChanges();
                }
                return RedirectToAction("QLBaiViet");
            }    
        }
        public ActionResult ChiTiet(string id)
        {
            BaiViet bv = db.BaiViets.SingleOrDefault(n => n.maBaiViet == id);
            ViewBag.maBV = bv.maBaiViet;
            if(bv == null)
            {
                Response.StatusCode = 404;
                return null;
            }    
            return View(bv);
        }
        [HttpGet]
        public ActionResult Xoa(string id)
        {
            BaiViet bv = db.BaiViets.SingleOrDefault(n => n.maBaiViet == id);
            ViewBag.maBV = bv.maBaiViet;
            if (bv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(bv);
        }
        [HttpPost,ActionName("Xoa")]
        public ActionResult XacNhanXoa(string id)
        {
            BaiViet bv = db.BaiViets.SingleOrDefault(n => n.maBaiViet == id);
            ViewBag.maBV = bv.maBaiViet;
            if (bv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.BaiViets.Remove(bv);
            db.SaveChanges();
            return RedirectToAction("QLBaiViet");
        }


        public ActionResult GetTop()
        {
            db = new DocBaoEntities1();
            var data = db.BaiViets.OrderByDescending(x => x.tuaBaiViet).Take(10).ToList();
            return View(data);
        }
        public ActionResult Search(string query)
        {
            db = new DocBaoEntities1();
            List<BaiViet> list = db.BaiViets.Where(x =>
                x.tuaBaiViet.ToLower().Contains(query.ToLower())).ToList<BaiViet>();
            return PartialView(list);
        }

        public ActionResult Open()
        {
            string path = Server.MapPath("~/info.txt");
            string[] info = System.IO.File.ReadAllLines(path);
            BaiViet s = new BaiViet();

            ViewBag.hinhAnh = "~/Content/igmaes" + s.hinhAnh;
            return View("TrangChu");


        }
        //[Authorize(Users = "admin")]
        [SessionTimeout]
        public ActionResult QLBaiViet()
        {
            List<BaiViet> lst = db.BaiViets.ToList<BaiViet>();
            return View(lst);
        }

    }
}
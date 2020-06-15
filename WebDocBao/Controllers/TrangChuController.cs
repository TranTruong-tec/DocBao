using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocBao.Models;

namespace WebDocBao.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: TrangChu
        DocBaoEntities1 db = new DocBaoEntities1();
        // GET: TrangChu
        int pageSize = 4;

        public ActionResult TrangChu()
        {

            db = new DocBaoEntities1();
            ViewBag.pageCount = Math.Ceiling(1.0 * db.BaiViets.ToList().Count / pageSize);
            return View();

        }
        public ActionResult LoadBaiVietPartial(int pageNo, int pageSize = 4)
        {
            db = new DocBaoEntities1();
            var l = db.BaiViets.ToList();
            var model = l.Skip(pageNo * pageSize).Take(pageSize).ToList();
            return PartialView(model);
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
                ViewBag.BaiViet = "Không có bài viết nào thuộc chủ đề" + l.tenLoai;

            }

            ViewBag.lstLoaiBV = db.LoaiBaiViets.ToList();
            return View(lstBV);
        }

        public ActionResult Search(string query)
        {
            db = new DocBaoEntities1();
            List<BaiViet> list = db.BaiViets.Where(x =>
                x.tuaBaiViet.ToLower().Contains(query.ToLower())).ToList<BaiViet>();
            return PartialView(list);
        }
        
    }
}
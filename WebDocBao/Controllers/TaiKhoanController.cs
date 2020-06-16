using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocBao.Models;

namespace WebDocBao.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        DocBaoEntities1 db = new DocBaoEntities1();
        
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(TaiKhoan tk, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                db = new DocBaoEntities1();

                
                db.TaiKhoans.Add(tk);
                db.SaveChanges();
                return RedirectToAction("TrangChu", "TrangChu");

            }
            ViewBag.thongbao("Tài khoản đã tồn tại");
            return View();

        }
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(String strURL, FormCollection f)
        {

            string username = f["maTaiKhoan"].ToString();
            string password = f["matKhau"].ToString();

            if (CheckUser(username, password))
            {
                Session["maTaiKhoan"] = username;
                HttpCookie ck = new HttpCookie("myCookies");
                ck["name"] = username;
                Response.Cookies.Add(ck);

                ck.Expires = DateTime.Now.AddHours(6);
                return RedirectToAction("TrangChu", "TrangChu");


            }
            ViewBag.thongbao = "Tên tài khoản hoặc mật khẩu không đúng!";
            return Redirect(strURL);
        }
        private bool CheckUser(string username, string password)
        {
            using (var db = new DocBaoEntities1())
            {
                var kq = db.TaiKhoans.Where(x => x.maTaiKhoan == username && x.matKhau == password).ToList<TaiKhoan>();
                if (kq.Count() > 0)
                    return true;
                return false;

            }
        }

        public ActionResult DangXuat(string strURL)
        {
            Session.Abandon();
            if (Request.Cookies["myCookies"] != null)
            {
                HttpCookie myCookie = new HttpCookie("myCookies");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            return Redirect(strURL);
        }
        

    }
}

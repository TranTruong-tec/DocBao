using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocBao.Models;
using System.Web.Security;
using System.Security;
using WebDocBao.CheckSession;

namespace WebDocBao.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private  DocBaoEntities1 db = new DocBaoEntities1();
        public ActionResult Index()
        {
            List<TaiKhoan> lst = db.TaiKhoans.ToList<TaiKhoan>();
            return View(lst);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TaiKhoan tk,  FormCollection f)
        {

            string username = f["maTaiKhoan"].ToString();
            string password = f["matKhau"].ToString();

            if (CheckUser(username, password))
            {

                FormsAuthentication.SetAuthCookie(tk.maTaiKhoan, true);
                Session["maTaiKhoan"] = tk.maTaiKhoan.ToString();
                Session["matKhau"] = tk.matKhau.ToString();
                return RedirectToAction("Index", "Admin");

            }
            else
            {
                ModelState.AddModelError("", "Tên Đăng Nhập Hoặc Mật Khẩu Không Đúng !");
            }

            return View(tk);


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

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Admin");
        }
        public ActionResult Sua(string id)
        {
            var tk = db.TaiKhoans.Select(p => p).Where(p => p.maTaiKhoan == id).FirstOrDefault();
            return View(tk);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sua(TaiKhoan editTK)
        {

            try
            {

                var l = db.TaiKhoans.Select(p => p).Where(p => p.maTaiKhoan == editTK.maTaiKhoan).FirstOrDefault();
                l.tenDayDu = editTK.tenDayDu;
                l.loaiTaiKhoan = editTK.loaiTaiKhoan;
                l.email = editTK.email;
                l.diaChi = editTK.diaChi;
                l.sdt = editTK.sdt;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {

                return View();

            }

        }
        public ActionResult Xoa(string id)
        {
            var tk = db.TaiKhoans.Select(p => p).Where(p => p.maTaiKhoan == id).FirstOrDefault();
            return View(tk);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Xoa(string id, FormCollection f)
        {

            try
            {

                var tk = db.TaiKhoans.Select(p => p).Where(p => p.maTaiKhoan == id).FirstOrDefault();
                db.TaiKhoans.Remove(tk);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {

                return View();

            }

        }
    }
}
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
            return RedirectToAction("Index", "Admin");
        }
    }
}
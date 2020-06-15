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
        public ActionResult CapchaImage()
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0}+{1}=?", a, b);
            Session["Captcha"] = a + b;
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));
                int i, r, x, y;
                var pen = new Pen(Color.Yellow);
                for (i = 1; i < 10; i++)
                {
                    pen.Color = Color.FromArgb(
                       (rand.Next(0, 255)),
                       (rand.Next(0, 255)),
                       (rand.Next(0, 255))
                        );
                    r = rand.Next(0, (130 / 3));
                    x = rand.Next(0, 130);
                    y = rand.Next(0, 30);

                    gfx.DrawEllipse(pen, x - r, y - r, r, r);
                }
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }
            return img;
        }
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

                if (Session["Captcha"] == null || Session["Captcha"].ToString() != tk.Captcha)
                {
                    ModelState.AddModelError("Captcha", "Ket Qua Sai, Ban Hay Nhap Lai!");
                    return View(tk);

                }
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

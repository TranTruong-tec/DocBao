using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocBao.Controllers;

namespace WebDocBao.CheckSession
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
        
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["maTaiKhoan"] == null)
            {
                filterContext.Result = new RedirectResult("~/Admin/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
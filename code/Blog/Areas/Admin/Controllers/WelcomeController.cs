using Blog.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    public class WelcomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }


        public JsonResult Login(string loginname, string loginpwd)
        {
            LoginModel logm = new LoginModel { Name = loginname, Pwd = loginpwd };

            Session.Add("LG", logm);
            return Json(logm);
        }
    }
}

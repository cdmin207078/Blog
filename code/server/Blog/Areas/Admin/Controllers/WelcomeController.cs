using Blog.Areas.Admin.ViewModels;
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
            LoginViewModel logm = new LoginViewModel { Name = loginname, Pwd = loginpwd };

            Session.Add("LG", logm);
            return Json(logm);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIF.Blog.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {

        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return View();
        }
    }
}
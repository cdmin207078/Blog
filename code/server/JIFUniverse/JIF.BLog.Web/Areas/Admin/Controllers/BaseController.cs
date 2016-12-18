using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIF.Blog.Web.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public JsonResult Ok()
        {
            return Json(new { success = true });
        }

        public JsonResult Ok(string message)
        {
            return Json(new { success = true, message = message });
        }

        public JsonResult Ok<T>(T data)
        {
            return Json(new { success = true, data = data });
        }

        public JsonResult Ok<T>(string message, T data)
        {
            return Json(new { success = true, message = message, data = data });
        }


        public JsonResult Fail()
        {
            return Json(new { success = false });
        }

        public JsonResult Fail(string message)
        {
            return Json(new { success = false, message = message });
        }

        public JsonResult Fail<T>(T data)
        {
            return Json(new { success = false, data = data });
        }

        public JsonResult Fail<T>(string message, T data)
        {
            return Json(new { success = false, message = message, data = data });
        }
    }
}
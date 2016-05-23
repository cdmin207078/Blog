using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    //[OutputCache(Duration = 10)]
    public class ZenController : Controller
    {
        public ActionResult Now()
        {
            ViewBag.Now = DateTime.Now;
            return View();
        }

        //[OutputCache(Duration = 5)]
        [OutputCache(CacheProfile = "TestConfigCache")]
        public ActionResult NowWithCache_5()
        {
            ViewBag.Now = DateTime.Now;

            return PartialView("_Cache5");
        }

        //[OutputCache(Duration = 10)]
        public ActionResult NowWithCache_10()
        {
            ViewBag.Now = DateTime.Now;

            return PartialView("_Cache10");
        }
    }
}
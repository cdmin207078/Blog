using Blog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class ArticleController : Controller
    {
        ArticleService articleSvc = new ArticleService();

        public ActionResult Index(int id)
        {
            ViewBag.Article = articleSvc.Load(id);
            return View();
        }

        public string getTime()
        {
            return DateTime.Now.ToString();
        }
    }
}
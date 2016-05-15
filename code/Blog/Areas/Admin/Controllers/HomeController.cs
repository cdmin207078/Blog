using Blog.Models;
using Blog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        ArticleService articleSvc = new ArticleService();

        public ViewResult Index()
        {

            ViewBag.Title = "文章列表";

            ViewBag.Articles = articleSvc.Load();

            return View();
        }

        public ViewResult Edit(int id)
        {
            ViewBag.Title = "文章编辑";

            return View(articleSvc.Load(id));
        }

        [HttpPost]
        public JsonResult Edit(Article model)
        {
            return Json(new { Success = true, Message = string.Format("文章保存成功,操作时间 - {0}", DateTime.Now.ToString()) });
        }
    }
}

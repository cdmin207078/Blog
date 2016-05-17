using Blog.Models;
using Blog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        ArticleService articleSvc = new ArticleService();

        public ViewResult Index()
        {
            ViewBag.Title = "所有文章";

            ViewBag.Articles = articleSvc.Load(d => d.Id < 100);

            return View();
        }

        public ViewResult Edit(int id)
        {
            ViewBag.Title = "文章编辑";

            return View(articleSvc.Load(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(Article model)
        {
            model.CreateTime = DateTime.Now;
            model.MDContent = "没有Markdown 文档内容.";
            return Json(new { Success = true, Message = string.Format("文章保存成功,操作时间 - {0}", DateTime.Now.ToString()), Data = model });
        }
    }
}

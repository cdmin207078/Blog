using Blog.Areas.Admin.ViewModels;
using Blog.Models;
using Blog.Services;
using Blog.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        ArticleService _articleSvc = new ArticleService();
        CategoryService _categorySvc = new CategoryService();

        public ViewResult Index()
        {
            ViewBag.Title = "所有文章";

            ViewBag.Articles = _articleSvc.Load(d => d.Id < 100);

            return View();
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Title = "撰写文章";

            ArticleViewModel vm = new ArticleViewModel();

            vm.Categories = _categorySvc.Load();

            return View("Edit", vm);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "文章编辑";

            var article = _articleSvc.Load(id);
            if (article == null)
                return RedirectToAction("Index");

            ArticleViewModel vm = new ArticleViewModel();
            vm.Categories = _categorySvc.Load();

            vm.ArticleId = article.Id;
            vm.Title = article.Title;
            vm.Content = article.Content;
            vm.CategoryId = article.CategoryId;
            vm.Tag = article.Tag;
            vm.CreateTime = article.CreateTime;


            return View(vm);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(ArticleViewModel model)
        {
            if (model.ArticleId == 0)
            {
                //新增
                _articleSvc.Insert(new Article
                {
                    Title = model.Title,
                    Content = model.Content,
                    CategoryId = model.CategoryId,
                    Tag = model.Tag
                });
            }
            else
            {
                _articleSvc.Update(new Article
                {
                    Id = model.ArticleId,
                    Title = model.Title,
                    Content = model.Content,
                    CategoryId = model.CategoryId,
                    Tag = model.Tag
                });
            }
            return Json(new AppResult { Success = true, Message = "", Data = JsonConvert.SerializeObject(model) });
        }
    }
}

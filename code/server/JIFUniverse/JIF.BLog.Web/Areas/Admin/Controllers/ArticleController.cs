using JIF.Blog.Web.Areas.Admin.Models;
using JIF.Core;
using JIF.Core.Domain.Articles;
using JIF.Core.Domain.Articles.Dtos;
using JIF.Services.Articles;
using JIF.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace JIF.Blog.Web.Areas.Admin.Controllers
{
    public class ArticleController : AdminControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public ActionResult Index(int pageIndex = JIFConsts.sys_page_index, int pageSize = JIFConsts.sys_page_size)
        {
            var model = _articleService.Search(pageIndex: pageIndex, pageSize: pageSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var vm = new ArticleEditViewModel();
            vm.Categories = _articleService.GetCategories();

            return View("Edit", vm);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var vm = new ArticleEditViewModel();
            vm.Article = _articleService.Get(id);
            vm.Categories = _articleService.GetCategories();

            return View("Edit", vm);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, UpdateArticleInputDto model)
        {
            if (Request["AllowComments"] != null)
            {
                model.AllowComments = true;
            }

            if (Request["Published"] != null)
            {
                model.Published = true;
            }

            if (!id.HasValue)
            {
                _articleService.Insert(model);

                return RedirectToAction("Index", "Article");
            }
            else
            {
                _articleService.Update(model);

                return RedirectToAction("Edit", "Article", new { id = model.Id });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            _articleService.Delete(id);

            return AjaxOk();
        }

        // 文章分类列表页面
        [HttpGet]
        public ActionResult Categorylist()
        {
            var vm = _articleService.GetCategories().AsTree();

            return View(vm);
        }
    }
}
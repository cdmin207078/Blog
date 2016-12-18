using JIF.Core;
using JIF.Core.Domain.Articles;
using JIF.Core.Domain.Articles.Dtos;
using JIF.Services.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace JIF.Blog.Web.Areas.Admin.Controllers
{
    public class ArticleController : BaseController
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
            var article = new Article();
            return View("Edit", article);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var article = _articleService.Get(id);
            return View("Edit", article);
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

            return Ok();
        }
    }
}
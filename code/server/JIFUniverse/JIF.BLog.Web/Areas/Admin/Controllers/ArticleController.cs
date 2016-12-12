using JIF.Core.Domain.Articles;
using JIF.Core.Domain.Articles.Dtos;
using JIF.Services.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Index(int pageIndex = 1, int pageSize = 10)
        {
            var vm = _articleService.Search(pageIndex: pageIndex, pageSize: pageSize);
            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var article = new Article();

            if (id.HasValue)
            {
                article = _articleService.Get(id);
            }

            return View(article);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(UpdateArticleInputDto model)
        {
            if (model.Id == 0)
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
    }
}
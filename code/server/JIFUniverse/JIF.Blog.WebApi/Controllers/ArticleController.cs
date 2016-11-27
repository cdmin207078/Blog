using JIF.Blog.WebApi.Models;
using JIF.Core;
using JIF.Core.Domain.Articles;
using JIF.Services.Articles;
using System;
using System.Net;
using System.Web.Http;

namespace JIF.Blog.WebApi.Controllers
{
    public class ArticleController : BaseController
    {
        private IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }


        public IHttpActionResult Post(Article model)
        {
            _articleService.Insert(model);

            return Ok(model);
        }

        public IHttpActionResult Delete(int id)
        {
            return AjaxFail(id, "未实现");
        }

        public IHttpActionResult Put(int id, ArticleDto model)
        {
            var article = _articleService.Get(id);

            if (article == null)
                return NotFound();

            article.Title = model.Title;
            article.Content = model.Content;

            if (model.Published.HasValue)
            {
                article.Published = model.Published.Value;
            }

            if (model.IsDeleted.HasValue)
            {
                article.IsDeleted = model.IsDeleted.Value;
            }

            if (model.AllowComments.HasValue)
            {
                article.AllowComments = model.AllowComments.Value;
            }

            _articleService.Update(article);

            return AjaxOk(article);
        }

        [Authorize]
        public IHttpActionResult Get(int pageIndex = 1, int pageSize = 20)
        {
            return AjaxOk(_articleService.Search(pageIndex: pageIndex, pageSize: pageSize).ToPagedData());
        }

        public IHttpActionResult Get(int id)
        {
            return AjaxOk(_articleService.Get(id));
        }
    }
}

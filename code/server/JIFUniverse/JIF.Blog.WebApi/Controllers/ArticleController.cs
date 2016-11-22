using JIF.Core.Domain.Articles;
using JIF.Services.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JIF.Blog.WebApi.Controllers
{
    public class ArticleController : ApiController
    {
        private IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }


        public IHttpActionResult Add(Article model)
        {
            return Ok("Add");
        }


        [HttpGet]
        public IHttpActionResult Search()
        {
            return Ok("Search");
        }

        public IHttpActionResult GetArticleById()
        {
            return Ok("GetArticleById");
        }
    }
}

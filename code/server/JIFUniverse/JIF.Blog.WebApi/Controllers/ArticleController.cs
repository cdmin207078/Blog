using JIF.Core.Domain.Articles;
using JIF.Services.Articles;
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


        [HttpPost]
        public IHttpActionResult Add(Article model)
        {
            _articleService.Insert(model);
            return Ok(new { status = "ok", data = model });
        }

        [HttpGet]
        public IHttpActionResult Search()
        {
            return Ok(_articleService.Search(null));
        }

        [HttpGet]
        public IHttpActionResult GetArticleById()
        {
            return Ok("GetArticleById");
        }
    }
}

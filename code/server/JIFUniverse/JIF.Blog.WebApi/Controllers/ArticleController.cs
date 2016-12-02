
using JIF.Core;
using JIF.Core.Domain.Articles.Dtos;
using JIF.Services.Articles;
using System.Web.Http;

namespace JIF.Blog.WebApi.Controllers
{
    public class ArticlesController : BaseController
    {
        private IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        public IHttpActionResult Add(CreateArticleInputDto model)
        {
            _articleService.Insert(model);
            return AjaxOk("添加成功");
        }

        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            return AjaxFail("删除功能 - 未实现");
        }

        [HttpPost]
        public IHttpActionResult Update(UpdateArticleInputDto model)
        {


            _articleService.Update(model);
            return AjaxOk("修改成功");
        }

        [HttpGet]
        public IHttpActionResult List(int pageIndex = 1, int pageSize = 10)
        {
            return AjaxOk(_articleService.Search(pageIndex: pageIndex, pageSize: pageSize).ToPagedData());
        }

        [HttpGet]
        public IHttpActionResult Detail(int id)
        {
            return AjaxOk(_articleService.Get(id));
        }
    }
}

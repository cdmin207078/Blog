using JIF.Core.Domain.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIF.Core.Data;
using JIF.Core;
using System.Linq.Expressions;
using JIF.Core.Domain.Articles.Dtos;

namespace JIF.Services.Articles
{
    public partial class ArticleService : BaseService<Article>, IArticleService
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<ArticleCategory> _articleCategoryRepository;

        private readonly IWorkContext _workContext;

        public ArticleService(
            IRepository<Article> articleRepository,
            IRepository<ArticleCategory> articleCategoryRepository,
            IWorkContext workContext)
            : base(articleRepository)
        {
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _workContext = workContext;
        }

        public void Insert(CreateArticleInputDto model)
        {
            if (model == null)
            {
                throw new JIFException("article is null.");
            }

            if (string.IsNullOrWhiteSpace(model.Title)
                || string.IsNullOrWhiteSpace(model.Content))
            {
                throw new JIFException("文章 标题 / 内容 不能为空");
            }

            var entity = new Article
            {
                Title = model.Title,
                Content = model.Content,
                CategoryId = model.CategoryId,
                AllowComments = model.AllowComments,
                Published = model.Published,
                CreateTime = DateTime.Now,
                CreateUserId = _workContext.CurrentUser.Id
            };

            _articleRepository.Insert(entity);

        }

        public void Update(UpdateArticleInputDto model)
        {
            if (model == null)
            {
                throw new JIFException("article is null.");
            }

            if (string.IsNullOrWhiteSpace(model.Title)
                || string.IsNullOrWhiteSpace(model.Content))
            {
                throw new JIFException("article title / content must not null");
            }

            var entity = Get(model.Id);

            if (entity == null)
            {
                throw new JIFException("article is not exists.");
            }

            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.CategoryId = model.CategoryId;
            entity.AllowComments = model.AllowComments;
            entity.Published = model.Published;
            entity.IsDeleted = model.IsDeleted;
            entity.UpdateTime = DateTime.Now;
            entity.UpdateUserId = _workContext.CurrentUser.Id;

            _articleRepository.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _articleRepository.Get(id);

            if (entity == null)
                throw new JIFException("article is null.");

            _articleRepository.Delete(entity);
        }

        public IList<ArticleCategory> GetCategory()
        {
            return _articleCategoryRepository.Table.ToList();
        }
    }
}

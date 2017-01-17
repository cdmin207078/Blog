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

        #region article

        public void Insert(CreateArticleInputDto model)
        {
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

        #endregion

        #region article-category

        public void Insert(ArticleCategory category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new JIFException("分类名称不能为空");
            }

            if (_articleCategoryRepository.Table.Any(d => d.Name == category.Name && d.Id != category.Id))
            {
                throw new JIFException("分类名称已经存在");
            }

            _articleCategoryRepository.Insert(category);
        }

        public void Update(ArticleCategory category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new JIFException("分类名称不能为空");
            }

            if (_articleCategoryRepository.Table.Any(d => d.Name == category.Name && d.Id != category.Id))
            {
                throw new JIFException("分类名称已经存在");
            }

            _articleCategoryRepository.Update(category);
        }

        public ArticleCategory GetCategory(int id)
        {
            return _articleCategoryRepository.Get(id);
        }

        public IEnumerable<ArticleCategory> GetCategories()
        {
            return _articleCategoryRepository.Table.ToList();
        }

        public IEnumerable<ArticleCategory> GetCategoriesTree()
        {
            var source = GetCategories();

            var result = new List<ArticleCategory>();

            // 第一层直接查出
            result.AddRange(source.Where(d => d.ParentId == 0));

            foreach (var cur in result)
            {
                SetCategoriesTree(cur, source);
            }


            return result;
        }

        private void SetCategoriesTree(ArticleCategory cur, IEnumerable<ArticleCategory> categories)
        {
            foreach (var cate in categories)
            {

            }
        }

        #endregion
    }


    public static class ArticleServiceExtends
    {
        public static IEnumerable<ArticleCategory> AsTree(this IEnumerable<ArticleCategory> source)
        {
            return source;
        }
    }
}

using JIF.Core.Domain.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIF.Core.Data;
using JIF.Core;
using System.Linq.Expressions;

namespace JIF.Services.Articles
{
    public partial class ArticleService : BaseService<Article>, IArticleService
    {
        private IRepository<Article> _articleRepository;

        public ArticleService(IRepository<Article> articleRepository)
            : base(articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public Article Insert(Article model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("article is null.");
            }

            if (string.IsNullOrWhiteSpace(model.Title)
                || string.IsNullOrWhiteSpace(model.Content))
            {
                throw new ArgumentNullException("article title / content must not null");
            }

            model.CreateTime = DateTime.Now;
            //model.CreateUserId = 0;

            _articleRepository.Insert(model);

            return model;
        }

        public Article Update(Article model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("article is null.");
            }

            if (string.IsNullOrWhiteSpace(model.Title)
                || string.IsNullOrWhiteSpace(model.Content))
            {
                throw new ArgumentNullException("article title / content must not null");
            }

            model.UpdateTime = DateTime.Now;
            //model.UpdateUserId = 0;

            _articleRepository.Update(model);

            return model;
        }
    }
}

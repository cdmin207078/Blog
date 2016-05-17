using Blog.Models;
using Blog.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Services
{
    class ArticleService : BaseServices<Article>, IArticleService
    {
        IRepository<Article> repositores = new EfRepository<Article>();

        public ArticleService()
        {
            this._repositores = repositores;
        }

        public List<Article> Load(System.Linq.Expressions.Expression<Func<Article, bool>> whereLambda)
        {
            return _repositores.Table.Where(whereLambda).ToList();
        }
    }
}
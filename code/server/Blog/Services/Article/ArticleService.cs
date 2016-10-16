using Blog.Models;
using Blog.Repositores;
using Blog.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Services
{
    public class ArticleService : BaseServices<Article>, IArticleService
    {
        public ArticleService()
        {
            this._repositores = new EfRepository<Article>();
        }

        public List<Article> Load(System.Linq.Expressions.Expression<Func<Article, bool>> whereLambda)
        {
            return _repositores.Table.Where(whereLambda).OrderByDescending(d => d.Id).ToList();
        }

        public override void Insert(Article model)
        {
            if (string.IsNullOrWhiteSpace(model.Title))
                throw new ServiceException("标题不能为空");

            if (string.IsNullOrWhiteSpace(model.Content))
                throw new ServiceException("内容不能为空");

            model.CreateTime = DateTime.Now;

            base.Insert(model);
        }
    }
}
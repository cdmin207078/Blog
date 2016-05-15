using Blog.Models;
using Blog.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Services
{
    public class ArticleService : IArticleService
    {
        IRepository<Article> _articleRepository = new EfRepository<Article>();

        public void Insert(Article model)
        {
            throw new NotImplementedException();
        }

        public void Update(Article model)
        {
            throw new NotImplementedException();
        }

        public Article Load(int id)
        {
            return _articleRepository.Load(id);
        }

        public List<Article> Load()
        {
            return _articleRepository.Table.Where(d => d.Id < 1000).ToList();
        }
    }
}
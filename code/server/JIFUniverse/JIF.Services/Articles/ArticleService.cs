﻿using JIF.Core.Domain.Articles;
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
        private IRepository<Article> _articleRepository;

        public ArticleService(IRepository<Article> articleRepository)
            : base(articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public void Insert(CreateArticleInputDto model)
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

            var entity = new Article
            {
                Title = model.Title,
                Content = model.Content,
                CategoryId = model.CategoryId,
                AllowComments = model.AllowComments,
                Published = model.Published,
                CreateTime = DateTime.Now,
                //CreateUserId = 0
            };

            _articleRepository.Insert(entity);

        }

        public void Update(UpdateArticleInputDto model)
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

            var entity = Get(model.Id);

            if (entity == null)
            {
                throw new ArgumentNullException("article is not exists.");
            }

            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.CategoryId = model.CategoryId;
            entity.AllowComments = model.AllowComments;
            entity.Published = model.Published;
            entity.IsDeleted = model.IsDeleted;
            entity.UpdateTime = DateTime.Now;
            //entity.UpdateUserId = 0;

            _articleRepository.Update(entity);
        }
    }
}

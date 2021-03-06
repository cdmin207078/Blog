﻿using JIF.Core.Domain.Articles;
using JIF.Core.Domain.Articles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Services.Articles
{
    public partial interface IArticleService : IBaseService<Article>
    {
        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Insert(CreateArticleInputDto model);

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(UpdateArticleInputDto model);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);


        void Insert(ArticleCategory category);

        void Update(ArticleCategory category);

        /// <summary>
        /// 获取具体文章分类信息
        /// </summary>
        /// <returns></returns>
        ArticleCategory GetCategory(int id);

        /// <summary>
        /// 获取所有文章分类
        /// </summary>
        /// <returns></returns>
        IEnumerable<ArticleCategory> GetCategories();

        /// <summary>
        /// 获取所有文章分类 [属性数据结构]
        /// </summary>
        /// <returns></returns>
        IEnumerable<ArticleCategory> GetCategoriesTree();
    }
}

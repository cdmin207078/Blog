using JIF.Core.Domain.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIF.Blog.Web.Areas.Admin.Models
{
    public class ArticleEditViewModel
    {
        /// <summary>
        /// 文章信息
        /// </summary>
        public Article Article { get; set; } = new Article();

        /// <summary>
        /// 文章分类列表
        /// </summary>
        public IEnumerable<ArticleCategory> Categories { get; set; } = new List<ArticleCategory>();
    }
}
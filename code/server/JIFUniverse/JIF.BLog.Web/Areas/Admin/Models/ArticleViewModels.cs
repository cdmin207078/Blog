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
        public Article Article { get; set; }

        /// <summary>
        /// 文章分类列表
        /// </summary>
        public IList<ArticleCategory> Categories { get; set; }
    }
}
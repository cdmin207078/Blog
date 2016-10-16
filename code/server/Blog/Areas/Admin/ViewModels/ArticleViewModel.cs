using Blog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Areas.Admin.ViewModels
{
    public class ArticleViewModel
    {
        /// <summary>
        /// 文章编号
        /// </summary>
        [Required]
        public int ArticleId { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 所属分类
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 文章分类数据源
        /// </summary>
        public List<Category> Categories { get; set; }


        public ArticleViewModel()
        {
            Categories = new List<Category>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain.Articles
{
    public partial class ArticleCategory : BaseEntity
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分类排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 父级分类
        /// </summary>
        public int ParentCategoryId { get; set; }

    }
}

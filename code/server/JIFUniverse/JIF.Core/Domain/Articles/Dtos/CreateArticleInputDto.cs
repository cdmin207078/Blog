using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain.Articles.Dtos
{
    public class CreateArticleInputDto
    {
        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 正文内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否允许评论
        /// </summary>
        public bool AllowComments { get; set; }

        /// <summary>
        /// 是否已经发布
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// 所属分类编号
        /// </summary>
        public int CategoryId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain.Articles
{
    public partial class Article : BaseEntity, ISoftDelete
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

        public bool IsDeleted { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreateUserId { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int? UpdateUserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain.Articles
{
    public class ArticleComment : BaseEntity, ISoftDelete
    {
        /// <summary>
        /// 所属文章编号
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NikeName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 来源IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 访问来源
        /// </summary>
        public string Agent { get; set; }

        /// <summary>
        /// 评论者链接网址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 是否通过审核
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// 回复指定留言
        /// </summary>
        public int? ReplyForCommentId { get; set; }

        /// <summary>
        /// 回复指定用户
        /// </summary>
        public int? ReplyForUserId { get; set; }

        public string CreateTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}

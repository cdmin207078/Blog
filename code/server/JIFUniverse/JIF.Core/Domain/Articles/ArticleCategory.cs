using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain.Articles
{
    public partial class ArticleCategory : BaseEntity, ITreeObject<ArticleCategory>
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        //[JsonIgnore]
        /// <summary>
        /// 所属父级分类
        /// </summary>
        public virtual ArticleCategory Parent { get; set; }

        /// <summary>
        /// 所属父级分类编号
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 包含子分类列表
        /// </summary>
        public virtual IList<ArticleCategory> Subs { get; set; }

        /// <summary>
        /// 分类排序
        /// </summary>
        public int Order { get; set; }
    }
}

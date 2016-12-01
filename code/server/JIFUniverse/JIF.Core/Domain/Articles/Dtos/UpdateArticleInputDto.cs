using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain.Articles.Dtos
{
    public class UpdateArticleInputDto : CreateArticleInputDto
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}

using JIF.Core.Domain.Articles;
using JIF.Core.Domain.Articles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}

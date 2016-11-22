using JIF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Services
{
    public partial interface IBaseService<T> where T : BaseEntity
    {
        /// <summary>
        /// 根据主键编号获取实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(object id);

        /// <summary>
        /// 条件搜索, 返回分页结果
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IPagedList<T> Search(Expression<Func<T, bool>> whereLambda, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}

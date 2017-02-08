using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain
{
    public interface ITreeObject<T>
    {
        int Id { get; set; }

        /// <summary>
        /// 所属父类编号, 顶级分类 ParentId = 0
        /// </summary>
        int ParentId { get; set; }

        T Parent { get; set; }

        IList<T> Subs { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        int Order { get; set; }

    }
}

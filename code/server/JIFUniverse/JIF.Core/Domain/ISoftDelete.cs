using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain
{
    public interface ISoftDelete
    {
        /// <summary>
        /// 记录是否被 '删除'
        /// </summary>
        bool IsDeleted { get; set; }
    }
}

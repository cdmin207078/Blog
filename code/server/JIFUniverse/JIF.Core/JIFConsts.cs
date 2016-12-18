using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core
{
    public static class JIFConsts
    {
        #region JIF System Setting

        /// <summary>
        /// 系统默认用户ID
        /// </summary>
        public const int sys_defaultUID = 1;

        /// <summary>
        /// 默认页码, 第一页
        /// </summary>
        public const int sys_page_index = 1;

        /// <summary>
        /// 默认页数据行, 20行
        /// </summary>
        public const int sys_page_size = 20;

        /// <summary>
        /// 每页显示数据条数
        /// </summary>
        public enum sys_page_size_type
        {
            S10 = 10,
            S20 = 20,
            S50 = 50,
            S100 = 100
        }

        #endregion

        #region Format DateTime

        /// <summary>
        /// 默认时间格式化
        /// </summary>
        public const string datetime_normal = "yyyy-MM-dd HH:mm:ss";

        #endregion
    }
}

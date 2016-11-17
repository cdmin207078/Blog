using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain.Users
{
    public partial class User : __Base_CU
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NikeName { get; set; }
    }
}

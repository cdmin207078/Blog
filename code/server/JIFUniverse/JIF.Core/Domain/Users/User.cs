using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain.Users
{
    public partial class User : BaseEntity
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NikeName { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 联络电话 (固话座机)
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string CellPhone { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreateUserId { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int? UpdateUserId { get; set; }
    }
}

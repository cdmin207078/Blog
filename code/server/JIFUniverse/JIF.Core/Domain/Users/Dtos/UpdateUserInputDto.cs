using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain.Users.Dtos
{
    public class UpdateUserInputDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool? Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 登陆 - 密码
        /// </summary>
        public string Password { get; set; }

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
        public string Tel { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// qq号码
        /// </summary>
        public int? QQ { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}

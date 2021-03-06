﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain.Users
{
    public partial class User : BaseEntity
    {
        /// <summary>
        /// 性别
        /// </summary>
        public bool? Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 登陆 - 账号
        /// </summary>
        public string Account { get; set; }

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
        /// 邮箱 - 是否验证通过
        /// </summary>
        public bool Email_Valid { get; set; }

        /// <summary>
        /// 手机 - 是否验证通过
        /// </summary>
        public bool Mobile_Valid { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 最后一次登陆时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最后一次登陆IP
        /// </summary>
        public string LastLoginIP { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreateUserId { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int? UpdateUserId { get; set; }
    }
}

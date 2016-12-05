using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIF.Blog.WebApi.Models
{
    /// <summary>
    /// 用户登录数据集
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 登录帐号
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "帐号不能为空")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Captcha { get; set; }
    }


    /// <summary>
    /// 用户注册数据集
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// 登录帐号
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "帐号不能为空")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "确认密码不能为空")]
        [Compare("Password", ErrorMessage = "两次密码不一致")]
        public string Confirm_password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Captcha { get; set; }
    }
}
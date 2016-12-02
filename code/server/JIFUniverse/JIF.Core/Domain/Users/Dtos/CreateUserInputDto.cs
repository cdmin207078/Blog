using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain.Users.Dtos
{
    public class CreateUserInputDto
    {
        /// <summary>
        /// 登陆 - 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 登陆 - 密码
        /// </summary>
        public string Password { get; set; }
    }
}

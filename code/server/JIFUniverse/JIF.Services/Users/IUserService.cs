using JIF.Core.Domain.Users;
using JIF.Core.Domain.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Services.Users
{
    public partial interface IUserService : IBaseService<User>
    {
        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="model"></param>
        void Insert(CreateUserInputDto model);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        void Update(UpdateUserInputDto model);

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        LoginOutputDto Login(LoginInputDto model);

        /// <summary>
        /// 注册用户
        /// </summary>
        void Register(RegisterInputDto model);
    }
}

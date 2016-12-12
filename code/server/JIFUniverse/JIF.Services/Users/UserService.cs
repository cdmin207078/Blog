using JIF.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIF.Core.Domain.Users.Dtos;
using JIF.Core.Data;
using JIF.Core;
using System.Security.Cryptography;

namespace JIF.Services.Users
{
    public partial class UserService : BaseService<User>, IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IWebHelper _webHelper;

        public UserService(IRepository<User> userRepository,
            IWebHelper webHelper)
            : base(userRepository)
        {
            _userRepository = userRepository;
            _webHelper = webHelper;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        public void Insert(CreateUserInputDto model)
        {
            if (model == null)
            {
                throw new JIFException("信息不能为空");
            }

            if (string.IsNullOrWhiteSpace(model.Account)
                || string.IsNullOrWhiteSpace(model.Password))
            {
                throw new JIFException("账号 / 密码 不能为空");
            }

            User entity = _userRepository.Table.FirstOrDefault(d => d.Account.ToLower().Trim() == model.Account.ToLower().Trim());

            if (entity != null)
            {
                throw new JIFException("账号:" + model.Account + ",已存在");
            }

            entity = new User();
            entity.CreateTime = DateTime.Now;
            entity.CreateUserId = JIFConsts.sys_defaultUID;
            entity.Account = model.Account;
            entity.Password = string.Format("{0}-{1}", model.Password, DateTime.Now.ToString(JIFConsts.datetime_normal));

            _userRepository.Insert(entity);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        public void Update(UpdateUserInputDto model)
        {
            if (model == null)
            {
                throw new JIFException("信息不能为空");
            }

            var entity = Get(model.Id);

            if (entity == null)
            {
                throw new JIFException("用户信息不存在");
            }

            entity.Sex = model.Sex;
            entity.Birthday = model.Birthday;
            entity.Password = model.Password;
            entity.NikeName = model.NikeName;
            entity.Email = model.Email;
            entity.Tel = model.Tel;
            entity.QQ = model.QQ;
            entity.IsDeleted = model.IsDeleted;

            #region  手机 | 邮箱, 若已经绑定. 则不能直接修改

            if (false == entity.Mobile_Valid)
            {
                entity.Mobile = model.Mobile;
            }

            if (false == entity.Email_Valid)
            {
                entity.Email = model.Email;
            }

            #endregion

            _userRepository.Update(entity);
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public LoginOutputDto Login(LoginInputDto model)
        {
            if (model == null)
            {
                throw new JIFException("信息不能为空");
            }

            if (string.IsNullOrWhiteSpace(model.Account)
                || string.IsNullOrWhiteSpace(model.Password))
            {
                throw new JIFException("账号 / 密码 不能为空");
            }

            var entity = _userRepository.Table.FirstOrDefault(d =>
                                d.Account.ToLower().Trim() == model.Account.ToLower().Trim()
                             && d.Password.ToLower().Trim() == model.Password.ToLower().Trim());

            if (entity == null)
                throw new JIFException("账号 / 密码 不正确");

            entity.LastLoginTime = DateTime.Now;
            entity.LastLoginIP = _webHelper.GetCurrentIpAddress();

            _userRepository.Update(entity);

            return new LoginOutputDto
            {
                UserId = entity.Id
            };
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        public void Register(RegisterInputDto model)
        {
            Insert(new CreateUserInputDto
            {
                Account = model.Account,
                Password = model.Password
            });
        }
    }
}

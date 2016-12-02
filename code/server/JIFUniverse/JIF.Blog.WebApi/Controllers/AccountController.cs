using JIF.Core.Domain.Users.Dtos;
using JIF.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace JIF.Blog.WebApi.Controllers
{
    public class AccountController : BaseController
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Register(string account, string password)
        {
            return AjaxOk(new { account, password });
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Login(LoginModel model)
        {
            return AjaxOk(model);
        }

        public class LoginModel
        {
            public string account { get; set; }
            public string password { get; set; }
        }

        /// <summary>
        /// 注销登陆
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Logout()
        {
            return Ok();
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Add()
        {
            return Ok();
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Update(UpdateUserInputDto model)
        {
            return Ok();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            return Ok();
        }

        /// <summary>
        /// 获取用户列表分页
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult List(int pageIndex = 1, int pageSize = 10)
        {
            return Ok();
        }

        /// <summary>
        /// 获取用户信息详情
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Detail(int id)
        {
            return Ok();
        }
    }
}

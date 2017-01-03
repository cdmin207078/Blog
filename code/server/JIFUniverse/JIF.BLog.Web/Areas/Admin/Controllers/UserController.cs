using JIF.Core;
using JIF.Core.Domain.Users;
using JIF.Services.Users;
using JIF.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIF.Blog.Web.Areas.Admin.Controllers
{
    public class UserController : AdminControllerBase
    {
        private readonly IWorkContext _workContext;
        private readonly IUserService _userService;

        public UserController(
            IWorkContext workContext,
            IUserService userService)
        {
            _workContext = workContext;
            _userService = userService;
        }

        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            User u = null;

            if (id.HasValue)
            {
                u = _userService.Get(id);
            }
            else
            {
                u = _workContext.CurrentUser;
            }
            return View(u);
        }

        [HttpPost]
        public ActionResult ModifyPwd(int uid, string originPwd, string newPwd)
        {
            if (uid == 0)
            {
                uid = _workContext.CurrentUser.Id;
            }

            _userService.ModifyPwd(uid, originPwd, newPwd);
            return AjaxOk();
        }
    }
}
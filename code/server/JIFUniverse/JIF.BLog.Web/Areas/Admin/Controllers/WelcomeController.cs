using JIF.Blog.Web.Areas.Admin.Models;
using JIF.Core.Domain.Users.Dtos;
using JIF.Services.Authentication;
using JIF.Services.Users;
using JIF.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JIF.Blog.Web.Areas.Admin.Controllers
{
    public class WelcomeController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;


        public WelcomeController(
            IAuthenticationService authenticationService,
            IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        // GET: Admin/Welcome
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model, string returnUrl)
        {
            var userInfo = _userService.Login(new LoginInputDto
            {
                Account = model.Account,
                Password = model.Password
            });

            if (userInfo != null)
            {
                var user = _userService.Get(userInfo.UserId);

                _authenticationService.SignIn(user, true);

                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return Redirect("/admin");
            }

            return View();
        }
    }
}
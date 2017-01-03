using JIF.Core;
using JIF.Core.Domain.Users;
using JIF.Services.Authentication;
using JIF.Services.Users;
using System;
using System.Web;

namespace JIF.Web.Framework
{
    public class WebWorkContext : IWorkContext
    {
        private readonly HttpContextBase _httpContext;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        private User _cachedUser;

        public WebWorkContext(
            HttpContextBase httpContext,
            IAuthenticationService authenticationService,
            IUserService userService)
        {
            _httpContext = httpContext;
            _authenticationService = authenticationService;
            _userService = userService;
        }


        public User CurrentUser
        {
            get
            {
                if (_cachedUser != null)
                    return _cachedUser;

                User user = null;

                // 针对后台job 调用,无 httpcontext 对象,则使用后台任务帐号
                // check whether request is made by a background task
                // in this case return built-in user record for background task

                // if (_httpContext == null)
                // {
                //     user = _userService.GetCustomerBySystemName(SystemCustomerNames.BackgroundTask);
                // }

                // registered user
                if (user == null)
                {
                    user = _authenticationService.GetAuthenticatedUser();
                }

                _cachedUser = user;

                return _cachedUser;
            }
        }

        public bool IsAdmin
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

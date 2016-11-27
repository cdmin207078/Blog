using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace JIF.Blog.WebApi.Controllers
{

    public class WelcomeController : BaseController
    {
        [HttpPost, Route("api/account/login")]
        public IHttpActionResult Login(string username, string password)
        {
            if (username != "admin" && password != "admin")
            {
                return AjaxFail(null, "");
            }

            FormsAuthenticationTicket token = new FormsAuthenticationTicket();



            return Ok("登录成功");
        }
    }
}

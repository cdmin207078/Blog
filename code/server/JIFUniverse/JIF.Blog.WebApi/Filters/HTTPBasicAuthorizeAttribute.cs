using System;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace JIF.Blog.WebApi.App_Start
{
    public class HTTPBasicAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                string userInfo = Encoding.Default.GetString(Convert.FromBase64String(actionContext.Request.Headers.Authorization.Parameter));
                //用户验证逻辑
                if (string.Equals(userInfo, string.Format("{0}:{1}", "admin", "admin")))
                {
                    IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            else
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            response.Headers.Add("WWW-Authenticate", "Basic");
            throw new HttpResponseException(response);
        }
    }
}
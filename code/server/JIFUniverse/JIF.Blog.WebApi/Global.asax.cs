using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace JIF.Blog.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 配置路由
            GlobalConfiguration.Configure(WebApiConfig.Register);


            var config = GlobalConfiguration.Configuration;
            // 注册依赖
            new DependencyRegistrar().RegisterDependencies(config);
        }
    }
}

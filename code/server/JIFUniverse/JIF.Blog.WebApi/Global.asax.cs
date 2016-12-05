using JIF.Blog.WebApi.Filters;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            // ef-mysql configuration
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());

            // 配置路由
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var config = GlobalConfiguration.Configuration;
            // 注册依赖
            new DependencyRegistrar().RegisterDependencies(config);

            // 全局model validate 过滤
            config.Filters.Add(new ValidateModelAttribute());
            config.Filters.Add(new AppExceptionAttribute());
        }
    }
}

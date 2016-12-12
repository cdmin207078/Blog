using Autofac;
using Autofac.Integration.WebApi;
using JIF.Core;
using JIF.Core.Data;
using JIF.EntityFramework;
using JIF.Services;
using JIF.Services.Articles;
using JIF.Services.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace JIF.Blog.WebApi
{
    public static class DependencyRegistrar
    {
        internal static void RegisterDependencies(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();


            //register HTTP context and other related stuff
            builder.Register(c => new HttpContextWrapper(HttpContext.Current) as HttpContextBase)
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();

            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            // register web api controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // register actofac filter provider.
            //builder.RegisterWebApiFilterProvider(config);

            // register dbcontext 
            builder.Register<DbContext>(c => new JIFDbContext("name=JIF.Blog.DB")).InstancePerLifetimeScope();

            // repositores
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            // services
            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(Assembly.Load("JIF.Services"))
            //    .Where(t => t.Name.EndsWith("Service"))
            //    .InstancePerLifetimeScope();




            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();


            // Set the dependency resolver to be Autofac
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
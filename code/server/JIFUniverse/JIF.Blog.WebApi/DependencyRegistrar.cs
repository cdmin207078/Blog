using Autofac;
using Autofac.Integration.WebApi;
using JIF.Core.Data;
using JIF.EntityFramework;
using JIF.Services;
using JIF.Services.Articles;
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
    public class DependencyRegistrar
    {
        internal void RegisterDependencies(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // register web api controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: register actofac filter provider.
            //builder.RegisterWebApiFilterProvider(config);


            // dbcontext 
            //builder.RegisterType<JIFDbContext>().As<DbContext>().InstancePerLifetimeScope();
            builder.Register<DbContext>(c => new JIFDbContext("name=JIF.Blog.DB")).InstancePerLifetimeScope();

            // repositores
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            // services
            //builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>)).InstancePerLifetimeScope();
            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(Assembly.Load("JIF.Services"))
            //    .Where(t => t.Name.EndsWith("Service"))
            //    .InstancePerLifetimeScope();


            // Set the dependency resolver to be Autofac
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
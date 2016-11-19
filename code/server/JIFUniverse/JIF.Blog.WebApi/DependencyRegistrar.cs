using Autofac;
using Autofac.Integration.Mvc;
using JIF.Core.Data;
using JIF.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace JIF.Blog.WebApi
{
    public class DependencyRegistrar
    {
        protected void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // controllers
            builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterAssemblyTypes(typeof(WebApiApplication).Assembly).AsImplementedInterfaces();

            // dbcontext 
            builder.RegisterType<JIFDbContext>().As<DbContext>().InstancePerLifetimeScope();

            // repositores
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            // services
            //builder.RegisterAssemblyTypes(Assembly.Load("Waldorf.Application"))
            //    .Where(t => t.Name.EndsWith("Service"))
            //    .AsImplementedInterfaces();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
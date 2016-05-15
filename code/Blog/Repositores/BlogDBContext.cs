using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Blog.Repositores
{
    public class BlogDBContext : DbContext
    {
        static BlogDBContext()
        {
            Database.SetInitializer<BlogDBContext>(null);
        }


        public BlogDBContext()
            : base("name=DbConnection")
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
               .Where(type => false == string.IsNullOrWhiteSpace(type.Namespace)
                              && type.BaseType != null
                              && type.BaseType.IsGenericType
                              && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}
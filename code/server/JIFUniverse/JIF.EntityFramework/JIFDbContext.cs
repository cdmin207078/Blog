using JIF.Core.Domain.Articles;
using JIF.Core.Domain.Users;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.EntityFramework
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class JIFDbContext : DbContext
    {
        public JIFDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleCategory>().ToTable("ArticleCategories").HasKey(d => d.Id);
            modelBuilder.Entity<Article>().ToTable("Articles").HasKey(d => d.Id);
            modelBuilder.Entity<ArticleComment>().ToTable("ArticleComments").HasKey(d => d.Id);
            modelBuilder.Entity<User>().ToTable("Users").HasKey(d => d.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}

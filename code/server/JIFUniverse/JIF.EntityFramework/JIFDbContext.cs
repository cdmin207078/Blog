using JIF.Core.Domain.Articles;
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
            modelBuilder.Entity<Article>().ToTable("Article").HasKey(d => d.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}

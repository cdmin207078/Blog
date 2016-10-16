using Blog.Models;
using Blog.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Services
{
    public class CategoryService : BaseServices<Category>
    {
        public CategoryService()
        {
            this._repositores = new EfRepository<Category>();
        }

        public override List<Category> Load()
        {
            return base.Load().OrderBy(d => d.OrderIndex).ToList();
        }
    }
}
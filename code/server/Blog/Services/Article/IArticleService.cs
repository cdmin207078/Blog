using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services
{
    public interface IArticleService : IBaseServices<Article>
    {
        List<Article> Load(Expression<Func<Article, bool>> whereLambda);
    }
}

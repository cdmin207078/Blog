using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services
{
    interface IArticleService
    {
        void Insert(Article model);

        void Update(Article model);

        Article Load(int id);
    }
}

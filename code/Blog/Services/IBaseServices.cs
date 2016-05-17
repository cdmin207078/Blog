using Blog.Models;
using Blog.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services
{
    interface IBaseServices<TEntity> where TEntity : _BaseEntity
    {
        void Insert(TEntity model);

        void Update(TEntity model);

        TEntity Load(int id);
    }
}

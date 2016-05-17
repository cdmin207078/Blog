using Blog.Models;
using Blog.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Services
{
    class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : _BaseEntity
    {
        protected IRepository<TEntity> _repositores;

        public void Insert(TEntity model)
        {
            _repositores.Insert(model);
        }

        public void Update(TEntity model)
        {
            _repositores.Update(model);
        }

        public TEntity Load(int id)
        {
            return _repositores.Load(id);
        }
    }
}
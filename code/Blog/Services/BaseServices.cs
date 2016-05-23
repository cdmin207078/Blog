using Blog.Models;
using Blog.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Services
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : _BaseEntity
    {
        protected IRepository<TEntity> _repositores;

        public virtual void Insert(TEntity model)
        {
            _repositores.Insert(model);
        }

        public virtual void Update(TEntity model)
        {
            _repositores.Update(model);
        }

        public virtual TEntity Load(int id)
        {
            return _repositores.Load(id);
        }

        public virtual List<TEntity> Load()
        {
            return _repositores.Table.ToList();
        }
    }
}
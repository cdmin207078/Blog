using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Repositores
{
    public interface IRepository<TEntity> where TEntity : _BaseEntity
    {
        TEntity Load(int id);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        IQueryable<TEntity> Table { get; }

        IQueryable<TEntity> TableNoTracking { get; }
    }
}
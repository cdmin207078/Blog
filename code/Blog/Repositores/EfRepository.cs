using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Blog.Repositores
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : _BaseEntity
    {
        private readonly DbContext _context = new BlogDBContext();
        private IDbSet<TEntity> _entites;

        protected virtual IDbSet<TEntity> Entities
        {
            get
            {
                if (_entites == null)
                    _entites = _context.Set<TEntity>();

                return _entites;
            }
        }

        public TEntity Load(int id)
        {
            return this.Entities.Find(id);
        }

        public void Insert(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity missing in Inert.");

                this.Entities.Add(entity);

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity missing in Update.");

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public IQueryable<TEntity> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public IQueryable<TEntity> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }
    }
}
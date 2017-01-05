using JIF.Core;
using JIF.Core.Data;
using JIF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using JIF.Core.Domain;

namespace JIF.Services
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public IQueryable<T> Tables
        {
            get
            {
                return _repository.Table;
            }
        }

        public T Get(object id)
        {
            return _repository.Get(id);
        }

        public virtual IPagedList<T> Search(Expression<Func<T, bool>> whereLambda = null, int pageIndex = 1, int pageSize = int.MaxValue)
        {
            var query = _repository.Table;

            if (whereLambda != null)
                query = query.Where(whereLambda);

            return new PagedList<T>(query.OrderByDescending(d => d.Id), pageIndex, pageSize);
        }
    }
}

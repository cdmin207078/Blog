using JIF.Core;
using JIF.Core.Data;
using JIF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Services
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public T Get(object id)
        {
            return _repository.Get(id);
        }
    }
}

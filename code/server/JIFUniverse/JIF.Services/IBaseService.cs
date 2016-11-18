using JIF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Services
{
    public partial interface IBaseService<T> where T : BaseEntity
    {
        T Get(object id);
    }
}

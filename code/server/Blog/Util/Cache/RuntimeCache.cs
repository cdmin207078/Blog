using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Util.Cache
{
    public class RuntimeCache : ICache
    {
        public void Delete(string key)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            throw new NotImplementedException();
        }

        public void Put(string key, object obj)
        {
            throw new NotImplementedException();
        }

        public void Put(string key, object obj, int timeout)
        {
            throw new NotImplementedException();
        }
    }
}
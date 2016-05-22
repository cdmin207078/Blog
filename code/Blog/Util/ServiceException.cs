using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Util
{
    [Serializable]
    public class ServiceException : Exception
    {
        public ServiceException() : base() { }

        public ServiceException(string message) : base(message) { }

        public ServiceException(string message, Exception ex) : base(message, ex) { }
    }
}
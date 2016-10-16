using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Util
{
    public class AppResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
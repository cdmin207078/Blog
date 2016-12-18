using JIF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JIF.Blog.Web
{
    public class JIFExceptionAttribute : IExceptionFilter
    {


        //public override void OnException(ExceptionContext context)
        //{
        //    if (context.Exception is JIFException)
        //    {
        //        if (context.HttpContext.Request.IsAjaxRequest())
        //        {
        //            context.Result = new JsonResult
        //            {
        //                ContentEncoding = Encoding.UTF8,
        //                ContentType = "application/json",
        //                Data = new
        //                {
        //                    success = false,
        //                    message = context.Exception.Message
        //                }
        //            };

        //            context.ExceptionHandled = true;
        //        }
        //    }
        //}
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JIFException)
            {
                if (context.HttpContext.Request.IsAjaxRequest())
                {
                    context.Result = new JsonResult
                    {
                        ContentEncoding = Encoding.UTF8,
                        ContentType = "application/json",
                        Data = new
                        {
                            success = false,
                            message = context.Exception.Message,
                            date = DateTime.Now
                        }
                    };

                    context.ExceptionHandled = true;
                }
            }
        }
    }
}
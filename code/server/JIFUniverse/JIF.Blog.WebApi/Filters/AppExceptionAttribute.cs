using JIF.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace JIF.Blog.WebApi.Filters
{
    public class AppExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is JIFException)
            {
                var response = new HttpResponseMessage();

                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = context.Exception.Message
                }), Encoding.UTF8, "application/json");

                context.Response = response;
            }
        }
    }
}
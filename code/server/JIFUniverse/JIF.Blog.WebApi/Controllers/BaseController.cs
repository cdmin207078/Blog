using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace JIF.Blog.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        [NonAction]
        /// <summary>
        /// Ajax请求成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">返回数据</param>
        /// <param name="message">附加消息</param>
        /// <returns></returns>
        public IHttpActionResult AjaxOk(object data, string message = "")
        {
            return Ok(new
            {
                success = true,
                message = message,
                data = data,
            });
        }

        [NonAction]
        /// <summary>
        /// Ajax请求错误
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">返回数据</param>
        /// <param name="message">附加消息</param>
        /// <returns></returns>
        public IHttpActionResult AjaxFail(object data, string message = "")
        {
            return Ok(new
            {
                success = false,
                message = message,
                data = data,
            });
        }
    }
}

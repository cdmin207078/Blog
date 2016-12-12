﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JIF.Blog.WebApi.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                var response = new HttpResponseMessage();

                var err = new Dictionary<string, string[]>();

                foreach (var item in actionContext.ModelState)
                {
                    // 复合类型会以 {参数名.字段} 格式作为key, 例如 : model.name
                    if (item.Key.Contains('.'))
                    {
                        err.Add(item.Key.Split('.')[1], item.Value.Errors.Select(d => d.ErrorMessage).ToArray());
                    }
                    else
                    {
                        err.Add(item.Key, item.Value.Errors.Select(d => d.ErrorMessage).ToArray());
                    }
                }

                response.Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = err
                }), Encoding.UTF8, "application/json");


                response.StatusCode = HttpStatusCode.OK;

                actionContext.Response = response;
            }
        }
    }
}
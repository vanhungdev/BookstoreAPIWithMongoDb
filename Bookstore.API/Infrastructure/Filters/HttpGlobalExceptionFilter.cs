using Bookstore.Infrastructure.Enums;
using Bookstore.Infrastructure.Exceptions;
using Bookstore.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bookstore.API.Infrastructure.Filters
{

    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {

            string errorMessage = context.Exception.ToString();
            var response = ResultObject.Error(context.Exception.Message, errorMessage, code: ResultCode.ErrorException);
            context.Result = new ContentResult()
            {
                Content = JsonConvert.SerializeObject(response),
                ContentType = "application/json; charset=utf-8",
                StatusCode = (int)HttpStatusCode.OK
            };
            context.ExceptionHandled = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace WebAppService
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string exceptionMessage = string.Empty;
            if (actionExecutedContext.Exception.InnerException == null)
            {
                exceptionMessage = actionExecutedContext.Exception.Message;
            }
            else
            {
                exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
            }
            //We can log this exception message to the file or database.  
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(exceptionMessage)  
            };
            actionExecutedContext.Response = response;
        }
    }
}
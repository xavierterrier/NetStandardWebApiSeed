using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Newtonsoft.Json.Linq;
using WebAPIToolkit.Common;

namespace WebAPIToolkit.ErrorHandlers
{
    /// <summary>
    /// Catch all errors raised in a Controller call
    /// </summary>
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;

            actionExecutedContext.Response = HandleExceptions(exception);
        }

        public HttpResponseMessage HandleExceptions(Exception ex)
        {
            if (ex is ValidationException)
            {
                var typedEx = (ValidationException)ex;

                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new JsonContent(JObject.FromObject(typedEx.ErrorsDto)),
                    ReasonPhrase = "Validation exception"
                };
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("Internal Error.")
            };
        }
    }
}
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Newtonsoft.Json.Linq;

namespace WebAPIToolkit.Common.ErrorHandlers
{
    /// <summary>
    /// Catch all errors raised in a Controller call
    /// </summary>
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// An exception has been raised in a controller
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;

            actionExecutedContext.Response = HandleExceptions(exception);
        }

        /// <summary>
        /// Do something with exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public HttpResponseMessage HandleExceptions(Exception ex)
        {
            var exception = ex as BadRequestException;
            if (exception != null)
            {
                var typedEx = exception;

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
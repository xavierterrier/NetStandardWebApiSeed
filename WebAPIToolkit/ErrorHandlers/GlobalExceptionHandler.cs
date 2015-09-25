using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace WebAPIToolkit.ErrorHandlers
{
    // Catch all errors outside a controller call like :
    //  Controller constructor
    //  Errors inside action filter
    //  Exceptions because of routing conflict
    //  When Mediatypeformatter failed to negotiate content / Serialization problem
    //  Errors inside MessageHandlers
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public async override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            await Task.Factory.StartNew(() =>
            {
                const string genericErrorMessage = "Une erreur s'est produite, merci de réessayer ultérieurement.";
                var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new
                    {
                        Message = genericErrorMessage
                    });

                context.Result = new ResponseMessageResult(response);
            }, cancellationToken);
        }
    }
}
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace WebAPIToolkit.Common.ErrorHandlers
{

    /// <summary>
    /// Catch all errors outside a controller call like :
    /// Controller constructor
    /// Errors inside action filter
    /// Exceptions because of routing conflict
    /// When Mediatypeformatter failed to negotiate content / Serialization problem
    /// Errors inside MessageHandlers
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler
    {
        /// <summary>
        /// Handle unexepcted exception outside a controller call
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
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
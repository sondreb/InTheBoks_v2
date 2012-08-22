using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Filters;

namespace InTheBoks.Web
{
    public class UnhandledExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;

            var exType = context.Exception.GetType();

            if (exType == typeof(UnauthorizedAccessException))
                status = HttpStatusCode.Unauthorized;
            else if (exType == typeof(ArgumentException))
                status = HttpStatusCode.NotFound;
            else if (exType == typeof(ServiceUnavailableExceptions))
                status = HttpStatusCode.ServiceUnavailable;

            var apiError = new ApiMessageError() { message = context.Exception.Message };

            // create a new response and attach our ApiError object
            // which now gets returned on ANY exception result
            var errorResponse = context.Request.CreateResponse<ApiMessageError>(status, apiError);
            context.Response = errorResponse;

            base.OnException(context);
        }
    }
}

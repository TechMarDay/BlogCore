using Common.Exceptions;
using Common.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Web.Controls;

namespace Web.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        public GlobalExceptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            LogHelper.GetLogger().Error(string.Format("{0} - {1}", context.Exception.StackTrace, context.Exception.Message));

            if (context.Exception.GetType() == typeof(NotPermissionException))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                var result = new ApiResult()
                {
                    Result = -1,
                    Data = null,
                    ErrorMessage = context.Exception.Message
                };
                context.Result = new ObjectResult(result);
            }
            else if (context.Exception.GetType() == typeof(BusinessException))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                var result = new ApiResult()
                {
                    Result = -1,
                    Data = ((BusinessException)context.Exception).Data,
                    ErrorMessage = context.Exception.Message
                };
                context.Result = new ObjectResult(result);
            }
            else if (context.Exception.GetType() == typeof(BusinessWarningException))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                var result = new ApiResult()
                {
                    Result = 1,
                    Data = ((BusinessWarningException)context.Exception).DataTranfer,
                    ErrorMessage = context.Exception.Message
                };
                context.Result = new ObjectResult(result);
            }
            else
            {
                var result = new ApiResult()
                {
                    Result = -2,
                    Data = null,
                    ErrorMessage = "An error occur.Try it again."
                };
                if (_env.IsDevelopment())
                {
                    result.ErrorMessage = context.Exception.Message;
                }

                context.Result = new ObjectResult(result);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            }

            context.ExceptionHandled = true;
        }
    }
}

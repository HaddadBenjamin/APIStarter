using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReadModel.ElasticSearch.Domain.Exceptions;

namespace ReadModel.API.Filters
{
    public class ExceptionHandlerFilter : ExceptionFilterAttribute
    {
        private static readonly Dictionary<Type, HttpStatusCode> ExceptionTypeToHttpStatus = new Dictionary<Type, HttpStatusCode>
        {
            { typeof(NotFoundException), HttpStatusCode.NotFound },
            { typeof(UnauthorizedException), HttpStatusCode.Unauthorized },
            { typeof(GoneException), HttpStatusCode.Gone },
            { typeof(BadRequestException), HttpStatusCode.BadRequest },
        };

        public override async Task OnExceptionAsync(ExceptionContext exceptionContext)
        {
            var exception = exceptionContext.Exception.Demystify();
            var responseHttpStatus = ExceptionTypeToHttpStatus.GetValueOrDefault(exception.GetType(), HttpStatusCode.InternalServerError);

            exceptionContext.Result = new JsonResult(exception.ToString()) { StatusCode = (int)responseHttpStatus };

            await Task.CompletedTask;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using APIStarter.Domain.CQRS.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIStarter.Application.Filters
{
    public class ExceptionHandlerFilter : ExceptionFilterAttribute
    {
        private static readonly Dictionary<Type, HttpStatusCode> ExceptionTypeToHttpStatus = new Dictionary<Type, HttpStatusCode>
        {
            { typeof(AggregateNotFoundException), HttpStatusCode.NotFound }
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

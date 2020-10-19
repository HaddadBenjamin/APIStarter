using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WriteModel.Domain.CQRS.Exceptions;
using WriteModel.Domain.Exceptions;

namespace APIStarter.Application.Filters
{
    public class ExceptionHandlerFilter : ExceptionFilterAttribute
    {
        private static readonly Dictionary<Type, HttpStatusCode> ExceptionTypeToHttpStatus = new Dictionary<Type, HttpStatusCode>
        {
            { typeof(AggregateNotFoundException), HttpStatusCode.NotFound },
            { typeof(NotFoundException), HttpStatusCode.NotFound },
            { typeof(UnauthorizedException), HttpStatusCode.Unauthorized },
            { typeof(GoneException), HttpStatusCode.Gone },
            { typeof(BadRequestException), HttpStatusCode.BadRequest },
        };

        public override void OnException(ExceptionContext exceptionContext)
        {
            var exception = exceptionContext.Exception.Demystify();
            var responseHttpStatus = ExceptionTypeToHttpStatus.GetValueOrDefault(exception.GetType(), HttpStatusCode.InternalServerError);

            exceptionContext.Result = new JsonResult(exception.ToString()) { StatusCode = (int)responseHttpStatus };
        }
    }
}

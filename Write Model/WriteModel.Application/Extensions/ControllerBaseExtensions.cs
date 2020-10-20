using System;
using Microsoft.AspNetCore.Mvc;
using WriteModel.Domain.Tools.REST;

namespace APIStarter.Application.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static CreatedResult Created(this ControllerBase controller, Guid id)
        {
            var request = controller.HttpContext.Request;

            return controller.Created($"{request.Host}{request.Path}{request.QueryString}/{id}", new CreatedRestResource { Id = id });
        }
    }
}
using System;
using System.Threading.Tasks;
using APIStarter.Application.Example.Dtos;
using APIStarter.Domain.CQRS.Interfaces;
using APIStarter.Domain.ExampleToDelete.Commands;
using APIStarter.Domain.ExampleToDelete.Queries;
using Microsoft.AspNetCore.Mvc;

namespace APIStarter.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateItemDto dto)
        {
            var command = new CreateItem
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Locations = dto.Locations.Split(',')
            };

            await _mediator.SendCommand(command);

            return Ok(Guid.NewGuid());
            //return Created($"{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}/{command.Id}", command.Id);
        }

        [HttpPut]
        [Route("{itemId:guid}")]
        public async Task<IActionResult> Update(UpdateItemDto dto, [FromRoute] Guid itemId)
        {
            var command = new UpdateItem
            {
                Id = itemId,
                Name = dto.Name,
                Locations = dto.Locations.Split(',')
            };

            await _mediator.SendCommand(command);

            return Ok();
        }

        [HttpDelete]
        [Route("{itemId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid itemId)
        {
            var command = new DeleteItem { Id = itemId };

            await _mediator.SendCommand(command);

            return Ok();
        }

        [HttpGet]
        [Route("{itemId:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid itemId) => Ok(await _mediator.SendQuery(new GetItem { Id = itemId }));

        [HttpGet]
        [Route("getbyname")]
        public async Task<IActionResult> GetByName([FromQuery] string name) => Ok(await _mediator.SendQuery(new GetItemByName { Name = name }));
    }
}

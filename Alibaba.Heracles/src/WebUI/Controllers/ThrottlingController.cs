using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Alibaba.Heracles.Application.Throttlings.Commands.Create;
using Alibaba.Heracles.Application.Throttlings.Commands.Delete;
using Alibaba.Heracles.Application.Throttlings.Commands.Update;
using Alibaba.Heracles.Application.Throttlings.Queries.GetAll;
using Alibaba.Heracles.Application.Throttlings.Queries.GetSingle;

namespace Alibaba.Heracles.WebUI.Controllers
{
    public class ThrottlingController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllThrottlings()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetThrottlingById {Id = id}));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateThrottlingCommand command)
        {
            // maybe return created
            return Ok(await Mediator.Send(command));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateThrottlingCommand command)
        {
            command.Id = id;
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteThrottlingCommand {Id = id});

            return NoContent();
        }
    }
}
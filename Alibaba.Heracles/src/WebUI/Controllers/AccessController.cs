using System.Threading;
using System.Threading.Tasks;
using Alibaba.Heracles.Application.Throttlings.Commands.CheckAccess;
using Microsoft.AspNetCore.Mvc;

namespace Alibaba.Heracles.WebUI.Controllers
{
    public class AccessController : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> Check([FromBody] CheckAccessCommand command,
            CancellationToken token)
        {
            try
            {
                await Mediator.Send(command, token);
                return Ok();
            }
            catch (TooManyRequestsException e)
            {
                return new StatusCodeResult(429);
            }
        }
    }
}
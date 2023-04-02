using Application.Features.Clients.Commands.CreateClientCommand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    /// <ClientController>
    /// Controller for Client the version of the Api inheriting from BaseApiController is placed. 
    /// </ClientController>
    [ApiVersion("1.0")]
    public class ClientController : BaseApiController
    {
        /// <Post>
        /// 
        /// </Post>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateClientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}

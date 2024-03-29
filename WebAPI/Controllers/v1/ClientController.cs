﻿using Application.DTOs;
using Application.Features.Clients.Commands.CreateClientCommand;
using Application.Features.Clients.Commands.DeleteClientCommand;
using Application.Features.Clients.Commands.UpdateClientCommand;
using Application.Features.Clients.Queries.GetAllClients;
using Application.Features.Clients.Queries.GetClientById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    /// <ClientController>
    /// Controller for Client the version of the Api inheriting from BaseApiController is placed. 
    /// </ClientController>
    [ApiVersion("1.0")]
    public class ClientController : BaseApiController
    {
        /// <Get>
        /// Get type controller to Get all client
        /// </Get>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetAllClientParametersDto getAllClientParameters)
        {
            return Ok(await Mediator.Send(new GetAllClientsQuery { getAllClientParametersDto = getAllClientParameters }));
        }
        /// <Get>
        /// Get type controller to Get by Id a client
        /// </Get>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetClientByIdQuery { UserId = id }));
        }
        /// <Post>
        /// Post type controller to create a client
        /// </Post>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateClientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <Put>
        /// Put type controller to update a client
        /// </Put>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateClientCommand command)
        {
            if (id != command.userDto.UserId)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }
        /// <Delete>
        /// Delete type controller to delete a client
        /// </Delete>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteClienteCommand { UserId = id }));
        }
    }
}

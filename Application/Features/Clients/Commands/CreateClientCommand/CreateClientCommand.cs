using Application.Interfaces;
using Application.Wrappres;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clients.Commands.CreateClientCommand
{
    /// <CreateClientCommand>
    /// The class create client command is created where the mediator is inherited and redirected 
    /// to the implementation of this object, which acts as a handler for this command.
    /// It is also indicated that the response will return an integer that will be the id of the client created.
    /// </CreateClientCommand>
    public class CreateClientCommand : IRequest<Response<int>>
    {
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? SecondLastName { get; set; }
        public DateTime Birthday { get; set; }
        public string? Email { get; set; }
        public string? Addres { get; set; }
    }
    /// <CreateClientCommandHandle>
    /// when the command controller is called it will execute the handler or mediator "Handle".
    /// A handle class is created which contains an interface and this one has the input request and 
    /// the response where the createclientecommand is requested and it is answered with the generic 
    /// response class that as data will have the integer that will be the id of the created client.
    /// </CreateClientCommandHandle>
    public class CreateClientCommandHandle : IRequestHandler<CreateClientCommand, Response<int>>
    {
        /// <summary>
        /// _repositoryAsync will help us to bring all the functionalities with the DB context
        /// _mapper will help us to map and bring from the GeneralProfile class the mappings we have for the client class.
        /// </summary>
        private readonly IRepositoryAsync<Client> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateClientCommandHandle(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        /// <summary>
        /// First the clientCommand is mapped to a Client class and 
        /// the result is sent through the _repositoryAsync to query the DB.
        /// And the result is returned to the client
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Response<int>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var newRegister = _mapper.Map<Client>(request);
            var data = await _repositoryAsync.AddAsync(newRegister);

            return new Response<int>(data.ClientId);
        }
    }
}

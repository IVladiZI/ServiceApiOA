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
        public string? DocumentNumber { get; set; }
        public string? DocumentComplement { get; set; }
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
        private readonly IRepositoryAsync<User> _userRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateClientCommandHandle(IMapper mapper, IRepositoryAsync<User> userRepositoryAsync)
        {
            _mapper = mapper;
            _userRepositoryAsync = userRepositoryAsync;
        }
        /// <Handle>
        /// First the clientCommand is mapped to a Client class and 
        /// the result is sent through the _repositoryAsync to query the DB.
        /// And the result is returned to the client
        /// </Handle>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Response<int>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var newClient = _mapper.Map<Client>(request);
            User newUser = new()
            {
                Client = newClient,
                UserName = $"{newClient.Name[..1].ToUpper()}{newClient.Lastname.ToUpper()}",
                Password = newClient.DocumentNumber,
                Level = 1 
            };
            var dataUser = await _userRepositoryAsync.AddAsync(newUser);
            return new Response<int>(dataUser.UserId);
        }
    }
}

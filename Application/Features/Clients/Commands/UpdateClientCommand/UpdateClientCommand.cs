using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappres;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Clients.Commands.UpdateClientCommand
{
    /// <UpdateClientCommand>
    /// The class update client command is created where the mediator is inherited and redirected 
    /// to the implementation of this object, which acts as a handler for this command.
    /// It is also indicated that the response will return an integer that will be the id of the client updated.
    /// </UpdateClientCommand>
    public class UpdateClientCommand : IRequest<Response<int>>
    {
        public UserDto userDto { get; set; }
    }
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<User> _userRepositoryAsync;
        private readonly IMapper _mapper;

        public UpdateClientCommandHandler(IRepositoryAsync<User> userRepositoryAsync, IMapper mapper)
        {
            _mapper = mapper;
            _userRepositoryAsync = userRepositoryAsync;
        }
        /// <Handle>
        /// First the id of the client to be modified is searched and if it is found, the operation is performed.
        /// In the process of modifying the client, we affect the User entity because if we modify in cascade, it will also affect the client entity.
        /// </Handle>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Response<int>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepositoryAsync.GetBySpecAsync(new ClientSpecification(request.userDto.UserId)) ?? throw new KeyNotFoundException($"User not found for id: {request.userDto.UserId}");
            
            user.UserName = request.userDto.UserName;
            user.Password = request.userDto.Password;
            user.Level = request.userDto.Level;
            user.Client.DocumentNumber = request.userDto.Client.DocumentNumber;
            user.Client.DocumentComplement = request.userDto.Client.DocumentComplement;
            user.Client.Name = request.userDto.Client.Name;
            user.Client.Lastname = request.userDto.Client.Lastname;
            user.Client.SecondLastName = request.userDto.Client.SecondLastName;
            user.Client.Birthday = request.userDto.Client.Birthday;
            user.Client.Email = request.userDto.Client.Email;
            user.Client.Addres = request.userDto.Client.Addres;
            await _userRepositoryAsync.UpdateAsync(user);

            return new Response<int>(user.UserId, "client successfully updated");
        }
    }
}

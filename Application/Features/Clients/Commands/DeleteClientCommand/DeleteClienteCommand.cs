using Application.Interfaces;
using Application.Wrappres;
using Domain.Entities;
using MediatR;

namespace Application.Features.Clients.Commands.DeleteClientCommand
{
    /// <DeleteClienteCommand>
    /// The class deleted client command is created where the mediator is inherited and redirected 
    /// to the implementation of this object, which acts as a handler for this command.
    /// It is also indicated that the response will return an integer that will be the id of the client deleted.
    /// </DeleteClienteCommand>
    public class DeleteClienteCommand : IRequest<Response<int>>
    {
        public int UserId { get; set; }
    }
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClienteCommand, Response<int>>
    {
        private readonly IRepositoryAsync<User> _userRepositoryAsync;

        public DeleteClientCommandHandler(IRepositoryAsync<User> userRepositoryAsync)
        {
            _userRepositoryAsync = userRepositoryAsync;
        }
        /// <Handle>
        /// First the id of the client to be modified is searched and if it is found, the operation is performed.
        /// In the process of modifying the client, we affect the User entity because if we modify in cascade, it will also affect the client entity.
        /// </Handle>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Response<int>> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepositoryAsync.GetByIdAsync(request.UserId) ?? throw new KeyNotFoundException($"User not found for id: {request.UserId}");

            await _userRepositoryAsync.DeleteAsync(user);

            return new Response<int>(user.UserId, "client successfully deleted");
        }
    }


}

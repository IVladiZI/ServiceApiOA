using Application.DTOs.Users;
using Application.Interfaces;
using Application.Wrappres;
using MediatR;

namespace Application.Features.Authenticate.Commands.AuthenticateCommand
{
    /// <AuthenticateCommand>
    /// This class implements the command to authenticate a user by requesting the necessary login information.
    /// </AuthenticateCommand>
    public class AuthenticateCommand : IRequest<Response<AuthenticationResponse>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? IpAddress { get; set; }
    }
    /// <AuthenticateCommandHandler>
    /// This class implements the context and the logic for the connection with the DB, in this case the authentication.
    /// </AuthenticateCommandHandler>
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Response<AuthenticationResponse>>
    {
        private readonly IAccountService _accountService;

        public AuthenticateCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <Handle>
        /// This method calls the connection to the database to authenticate the user.
        /// </Handle>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Response<AuthenticationResponse>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.AuthenticateAsync(new AuthenticationRequest
            {
                Email = request.Email,
                Password = request.Password,
            },request.IpAddress);
        }
    }
}

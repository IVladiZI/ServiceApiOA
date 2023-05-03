using Application.DTOs.Users;
using Application.Interfaces;
using Application.Wrappres;
using MediatR;

namespace Application.Features.Authenticate.Commands.RegisterCommand
{
    /// <RegisterCommand>
    /// This class has the necessary values to create an account in the database
    /// </RegisterCommand>
    public class RegisterCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Origin { get; set; }
    }
    /// <registerCommandHandler>
    /// In this class we generate the connection with the service to register an account.
    /// </registerCommandHandler>
    public class registerCommandHandler : IRequestHandler<RegisterCommand, Response<string>>
    {
        private readonly IAccountService _accountService;

        public registerCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// With this method we call the registration of a user in the database
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.RegisterAsync(new RegisterRequest
            {
                Email = request.Email,
                Password = request.Password,
                UserName = request.UserName,
                Name = request.Name,
                LastName = request.LastName
            },request.Origin);
        }
    }
}

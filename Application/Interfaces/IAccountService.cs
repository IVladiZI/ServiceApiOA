using Application.DTOs.Users;
using Application.Wrappres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    /// <IAccountService>
    /// Methods for account interface registration and login
    /// </IAccountService>
    public interface IAccountService
    {
        //Login
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest authenticationRequest,string ipAddress);
        //register
        Task<Response<string>> RegisterAsync(RegisterRequest registerRequest, string ipAddress);
    }
}

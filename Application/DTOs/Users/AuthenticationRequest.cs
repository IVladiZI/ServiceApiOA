using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Users
{
    /// <AuthenticationRequest>
    /// Dto class for the authenticator user 
    /// </AuthenticationRequest>
    public class AuthenticationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

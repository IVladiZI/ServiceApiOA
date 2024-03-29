﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Users
{
    /// <AuthenticationResponse>
    /// Dto class for authenticator user response 
    /// </AuthenticationResponse>
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public string JWToken { get; set; }
        //With this we ignore the mapping in the DTO.
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}

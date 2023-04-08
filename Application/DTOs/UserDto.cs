using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs
{
    /// <UserDto>
    /// This class will be used for values that only the customer should know.
    /// </UserDto>
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int Level { get; set; }
        public ClientDto? Client { get; set; }
    }
}

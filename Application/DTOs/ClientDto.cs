using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    /// <ClientDto>
    /// This class will be used for values that only the customer should know.
    /// </ClientDto>
    public class ClientDto
    {
        public string? DocumentNumber { get; set; }
        public string? DocumentComplement { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string SecondLastName { get; set; }
        public DateTime Birthday { get; set; }
        public string? Email { get; set; }
        public string? Addres { get; set; }
    }
}

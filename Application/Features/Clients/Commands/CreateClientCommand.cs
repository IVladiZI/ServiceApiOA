using Application.Wrappres;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clients.Commands
{
    /// <CreateClientCommand>
    /// The class create client command is created where the mediator is inherited and redirected 
    /// to the implementation of this object, which acts as a handler for this command.
    /// </CreateClientCommand>
    public class CreateClientCommand : IRequest<Response<int>>
    {
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? SecondLastName { get; set; }
        public DateTime Birthday { get; set; }
        private int _age { get; set; }
        public string? Email { get; set; }
        public string? Addres { get; set; }
    }
}

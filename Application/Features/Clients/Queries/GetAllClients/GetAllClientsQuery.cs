using Application.DTOs;
using Application.Features.Clients.Queries.GetClientById;
using Application.Interfaces;
using Application.Wrappres;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsQuery : IRequest<PagedResponse<List<ClientDto>>>
    {
        public GetAllClientParametersDto getAllClientParametersDto { get; set; }

        public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, PagedResponse<List<ClientDto>>>
        {
            private readonly IRepositoryAsync<User> _userRepositoryAsync;
            private readonly IMapper _mapper;

            public GetAllClientsQueryHandler(IRepositoryAsync<User> userRepositoryAsync, IMapper mapper)
            {
                _userRepositoryAsync = userRepositoryAsync;
                _mapper = mapper;
            }

            public Task<PagedResponse<List<ClientDto>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}

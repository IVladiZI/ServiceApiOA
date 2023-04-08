using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappres;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Clients.Queries.GetAllClients
{
    /// <GetAllClientsQuery>
    /// In this class you configure and call methods to perform an action with the database.
    /// </GetAllClientsQuery>
    public class GetAllClientsQuery : IRequest<PagedResponse<List<UserDto>>>
    {
        public GetAllClientParametersDto getAllClientParametersDto { get; set; }
        /// <GetAllClientsQueryHandler>
        /// Another class is created where we will create the thread or flow that will follow 
        /// our validations as well as the connection and method calls with the database.
        /// </GetAllClientsQueryHandler>
        public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, PagedResponse<List<UserDto>>>
        {
            private readonly IRepositoryAsync<User> _userRepositoryAsync;
            private readonly IMapper _mapper;

            public GetAllClientsQueryHandler(IRepositoryAsync<User> userRepositoryAsync, IMapper mapper)
            {
                _userRepositoryAsync = userRepositoryAsync;
                _mapper = mapper;
            }
            /// <Handle>
            /// Method where the action is executed against the database depending on the 
            /// type of action to be executed and return the result of the action.
            /// </Handle>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<PagedResponse<List<UserDto>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
            {
                //We call the specification class because we want to make an advanced query with different
                //types of configuration for the pagination with the help of the Specification class.
                var clients = await _userRepositoryAsync.ListAsync(new PagedSpecification(request.getAllClientParametersDto));
                var clientsDto = _mapper.Map<List<UserDto>>(clients);
                return new PagedResponse<List<UserDto>>(clientsDto,request.getAllClientParametersDto.PageNumber, request.getAllClientParametersDto.PageSize);
            }
        }
    }
}

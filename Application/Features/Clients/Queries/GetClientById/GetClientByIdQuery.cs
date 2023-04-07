using Application.DTOs;
using Application.Interfaces;
using Application.Wrappres;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Clients.Queries.GetClientById
{
    /// <GetClientByIdQuery>
    /// This class will help us to bring the information of a customer filtering by an Id.
    /// </GetClientByIdQuery>
    public class GetClientByIdQuery : IRequest<Response<UserDto>>
    {
        public int UserId { get; set; }
    }
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery,Response<UserDto>>
    {
        private readonly IRepositoryAsync<User> _userRepositoryAsync;
        private readonly IMapper _mapper;

        public GetClientByIdQueryHandler(IRepositoryAsync<User> userRepositoryAsync, IMapper mapper)
        {
            _userRepositoryAsync = userRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<UserDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepositoryAsync.GetByIdAsync(request.UserId) ?? throw new KeyNotFoundException($"User not found for id: {request.UserId}");
            var userDto = _mapper.Map<UserDto>(user);
            return new Response<UserDto>(userDto);
        }
    }
}


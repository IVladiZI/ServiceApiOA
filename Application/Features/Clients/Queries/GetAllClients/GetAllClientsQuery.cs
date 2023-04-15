using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappres;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

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
            private readonly IDistributedCache _distributedCache;

            public GetAllClientsQueryHandler(IRepositoryAsync<User> userRepositoryAsync, IMapper mapper, IDistributedCache distributedCache)
            {
                _userRepositoryAsync = userRepositoryAsync;
                _mapper = mapper;
                _distributedCache = distributedCache;
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
                //here we create the variable that will become the unique key for the cache that will be used to query if
                //we have already made this query before and if so, send it to the cache but if not, query the database.
                var cacheKey = $"ListClients_{JsonConvert.SerializeObject(request)}";
                string serializedLisClients;
                var listClients = new List<User>();
                //Here we check if the cache key exists, i.e. if the query exists previously.
                var redisLisClients = await _distributedCache.GetAsync(cacheKey);
                //If the query exists then it will assemble what is in cache and if it
                //does not exist it will query the database and then cache it.
                if (redisLisClients != null)
                {
                    serializedLisClients = Encoding.UTF8.GetString(redisLisClients);
                    listClients = JsonConvert.DeserializeObject<List<User>>(serializedLisClients);
                }
                else
                {
                    listClients = await _userRepositoryAsync.ListAsync(new PagedSpecification(request.getAllClientParametersDto));
                    //we convert it to json and then to bits to cache it.
                    //We put more elements in the SerializeObject because when
                    //serializing it we had repeated references, that is to say User to client and
                    //in client to user and in user to client, this made that when serializing it in json it
                    //was a big loop, with this we will avoid that it brings the references.
                    serializedLisClients = JsonConvert.SerializeObject(listClients,Formatting.Indented,new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    redisLisClients = Encoding.UTF8.GetBytes(serializedLisClients);
                    //Here is configured with SetAbsoluteExpiration this method will help us to indicate
                    //the time that the cache will remain stored, with SetSlidingExpiration will help us to
                    //indicate the time that will be stored in cache while a query is not made. The first one
                    //must always be greater than the second one.
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await _distributedCache.SetAsync(cacheKey,redisLisClients,options);
                }

                //We call the specification class because we want to make an advanced query with different
                //types of configuration for the pagination with the help of the Specification class.
                var clientsDto = _mapper.Map<List<UserDto>>(listClients);
                return new PagedResponse<List<UserDto>>(clientsDto,request.getAllClientParametersDto.PageNumber, request.getAllClientParametersDto.PageSize);
            }
        }
    }
}

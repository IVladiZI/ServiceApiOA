using Application.DTOs;
using Application.Features.Clients.Commands.CreateClientCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class GeneralProfile : Profile
    {
        /// <GeneralProfile>
        /// This method helps us to map the classes, that is to say, the values that 
        /// ClientCommand has will be passed to Client, all those variables that have the same type of value.
        /// </GeneralProfile>
        public GeneralProfile() 
        {
            //Maping Entiti to Dto
            CreateMap<User, UserDto>();
            CreateMap<Client, ClientDto>();
            //Maping Dto to Entiti
            CreateMap<UserDto, User>();
            CreateMap<ClientDto, Client>();
            //Maping Comands to Entiti
            CreateMap<CreateClientCommand, Client>();
            CreateMap<CreateClientCommand, User>();
        }
    }
}

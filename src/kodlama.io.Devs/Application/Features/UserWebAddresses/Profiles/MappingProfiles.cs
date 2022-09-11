using Application.Features.UserWebAddresses.Commands.CreateUserWebAddress;
using Application.Features.UserWebAddresses.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserWebAddresses.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserWebAddress, CreateUserWebAddressCommand>().ReverseMap();
            CreateMap<UserWebAddress, CreatedUserWebAddressDto>().ReverseMap();            
            CreateMap<UserWebAddress, UpdatedUserWebAdressDto>().ReverseMap();
            CreateMap<UserWebAddress, DeletedUserWebAddressDto>().ReverseMap();
        }
    }
}

using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserOperationClaim, CreatedUserOperationClaimDto>().ForMember(c => c.UserFirstName, opt => opt.MapFrom(c => c.User.FirstName)).ForMember(c => c.UserLastName, opt => opt.MapFrom(c => c.User.LastName)).ForMember(c => c.OperationClaimName, opt => opt.MapFrom(c => c.OperationClaim.Name)).ReverseMap();

            CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();

            CreateMap<UserOperationClaim, DeletedUserOperationClaimDto>().ForMember(c => c.UserFirstName, opt => opt.MapFrom(c => c.User.FirstName)).ForMember(c => c.UserLastName, opt => opt.MapFrom(c => c.User.LastName)).ForMember(c => c.OperationClaimName, opt => opt.MapFrom(c => c.OperationClaim.Name)).ReverseMap();
            
        }
    }
}

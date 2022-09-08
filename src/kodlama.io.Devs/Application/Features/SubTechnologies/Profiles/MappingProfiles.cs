using Application.Features.SubTechnologies.Dtos;
using Application.Features.SubTechnologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SubTechnologies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SubTechnology, SubTechnologyListDto>()
                .ForMember(c => c.ProgrammingLanguageName, opt => opt.MapFrom(s => s.ProgrammingLanguage.Name))
                .ReverseMap();

            CreateMap<IPaginate<SubTechnology>, SubTechnologyListModel>().ReverseMap();
        }
    }
}

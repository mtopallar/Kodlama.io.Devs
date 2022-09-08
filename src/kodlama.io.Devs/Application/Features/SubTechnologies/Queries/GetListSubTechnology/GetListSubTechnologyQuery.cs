using Application.Features.SubTechnologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SubTechnologies.Queries.GetListSubTechnology
{
    public class GetListSubTechnologyQuery : IRequest<SubTechnologyListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListSubTechnologyQueryHandler : IRequestHandler<GetListSubTechnologyQuery, SubTechnologyListModel>
        {
            private readonly ISubTechnologyRepository _subTechnologyRepository;
            private readonly IMapper _mapper;

            public GetListSubTechnologyQueryHandler(ISubTechnologyRepository subTechnologyRepository, IMapper mapper)
            {
                _subTechnologyRepository = subTechnologyRepository;
                _mapper = mapper;
            }

            public async Task<SubTechnologyListModel> Handle(GetListSubTechnologyQuery request, CancellationToken cancellationToken)
            {
                IPaginate<SubTechnology> subTechnologies = await _subTechnologyRepository.GetListAsync(
                                                      include: s => s.Include(p => p.ProgrammingLanguage),
                                                      index: request.PageRequest.Page,
                                                      size: request.PageRequest.PageSize
                                                      );

                SubTechnologyListModel mappedSubTechnologies = _mapper.Map<SubTechnologyListModel>(subTechnologies);

                return mappedSubTechnologies;
            }
        }
    }
}

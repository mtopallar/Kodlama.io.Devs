using Application.Features.SubTechnologies.Dtos;
using Application.Features.SubTechnologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SubTechnologies.Commands.CreateSubTechnology
{
    public class CreateSubTechnologyCommand : IRequest<CreatedSubTechnologyDto>
    {
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class CreateSubTechnologyCommandHandler : IRequestHandler<CreateSubTechnologyCommand, CreatedSubTechnologyDto>
        {
            private readonly ISubTechnologyRepository _subTechnologyRepository;
            private readonly IMapper _mapper;
            private readonly SubTechnologyBusinessRules _subTechnologyBusinessRules;

            public CreateSubTechnologyCommandHandler(ISubTechnologyRepository subTechnologyRepository, IMapper mapper, SubTechnologyBusinessRules subTechnologyBusinessRules)
            {
                _subTechnologyRepository = subTechnologyRepository;
                _mapper = mapper;
                _subTechnologyBusinessRules = subTechnologyBusinessRules;
            }

            public async Task<CreatedSubTechnologyDto> Handle(CreateSubTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _subTechnologyBusinessRules.SubTechnologyNameCanNotBeDuplicatedWhenInserted(request.ProgrammingLanguageId, request.Name);

                SubTechnology mappedSubTechnology = _mapper.Map<SubTechnology>(request);
                SubTechnology createdSubTechnology = await _subTechnologyRepository.AddAsync(mappedSubTechnology);
                CreatedSubTechnologyDto createdSubTechnologyDto = _mapper.Map<CreatedSubTechnologyDto>(createdSubTechnology);

                return createdSubTechnologyDto;
            }
        }
    }
}

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

namespace Application.Features.SubTechnologies.Commands.UpdateSubTechnology
{
    public class UpdateSubTechnologyCommand : IRequest<UpdatedSubTechnologyDto>
    {
        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class UpdateSubTechnologyCommandHandler : IRequestHandler<UpdateSubTechnologyCommand, UpdatedSubTechnologyDto>
        {
            private readonly ISubTechnologyRepository _subTechnologyRepository;
            private readonly IMapper _mapper;
            private readonly SubTechnologyBusinessRules _subTechnologyBusinessRules;

            public UpdateSubTechnologyCommandHandler(ISubTechnologyRepository subTechnologyRepository, IMapper mapper, SubTechnologyBusinessRules subTechnologyBusinessRules)
            {
                _subTechnologyRepository = subTechnologyRepository;
                _mapper = mapper;
                _subTechnologyBusinessRules = subTechnologyBusinessRules;
            }

            public async Task<UpdatedSubTechnologyDto> Handle(UpdateSubTechnologyCommand request, CancellationToken cancellationToken)
            {
                SubTechnology? tryFindSubTechNolojiForUpdate = await _subTechnologyRepository.GetAsync(s => s.Id == request.Id);

                _subTechnologyBusinessRules.SubTechnologyExistsWhenRequested(tryFindSubTechNolojiForUpdate);

                tryFindSubTechNolojiForUpdate.ProgrammingLanguageId = request.ProgrammingLanguageId;
                tryFindSubTechNolojiForUpdate.Name = request.Name;

                await _subTechnologyBusinessRules.SubTechnologyNameCanNotBeDuplicatedWhenUpdated(tryFindSubTechNolojiForUpdate);

                SubTechnology updatedSubTechnology = await _subTechnologyRepository.UpdateAsync(tryFindSubTechNolojiForUpdate);
                UpdatedSubTechnologyDto mappedSubTechnology = _mapper.Map<UpdatedSubTechnologyDto>(updatedSubTechnology);

                return mappedSubTechnology;
            }
        }
    }
}

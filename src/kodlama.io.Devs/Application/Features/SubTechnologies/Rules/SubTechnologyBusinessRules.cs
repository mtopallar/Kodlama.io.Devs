using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SubTechnologies.Rules
{
    public class SubTechnologyBusinessRules
    {
        private readonly ISubTechnologyRepository _subTechnologyRepository;

        public SubTechnologyBusinessRules(ISubTechnologyRepository subTechnologyRepository)
        {
            _subTechnologyRepository = subTechnologyRepository;
        }

        public async Task SubTechnologyNameCanNotBeDuplicatedWhenInserted(int programmingLanguageId,string name)
        {
            IPaginate<SubTechnology> result = await _subTechnologyRepository.GetListAsync(
                s => s.ProgrammingLanguageId == programmingLanguageId && s.Name == name);
            if (result.Items.Any()) throw new BusinessException("Sub technology already exists.");
        }

        public async Task SubTechnologyNameCanNotBeDuplicatedWhenUpdated(SubTechnology subTechnology)
        {
            IPaginate<SubTechnology> result = await _subTechnologyRepository.GetListAsync(s=> s.ProgrammingLanguageId == subTechnology.ProgrammingLanguageId && s.Name == subTechnology.Name && s.Id != subTechnology.Id);

            if (result.Items.Any()) throw new BusinessException("The sub technology you're trying to update is already exists.");
        }

        public void SubTechnologyExistsWhenRequested(SubTechnology subTechnology)
        {
            if (subTechnology == null) throw new BusinessException("Requested sub technology doesn't exists.");
        }
    }
}

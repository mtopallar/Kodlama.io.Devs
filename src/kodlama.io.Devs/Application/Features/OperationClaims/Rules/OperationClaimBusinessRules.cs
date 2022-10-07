using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task OperationClaimCanNotBuDuplicatedWhenInsetred(string operationClaimName)
        {
            IPaginate<OperationClaim> result = await _operationClaimRepository.GetListAsync(o => o.Name == operationClaimName);
            if (result.Items.Any()) throw new BusinessException("Operation claim name exists.");
        }

        public void OperationClaimShouldExistWhenRequesed(OperationClaim operationClaim)
        {
            if (operationClaim == null) throw new BusinessException("Requested operation claim does not exists.");
        }

        public async Task OperationClaimNameCanNotDuplicateWhenUpdated(OperationClaim operationClaim)
        {
            OperationClaim? result = await _operationClaimRepository.GetAsync(o => o.Name == operationClaim.Name && o.Id != operationClaim.Id);
            if (result != null) throw new BusinessException("Operation claim you're trying to update already exists.");
        }
    }
}

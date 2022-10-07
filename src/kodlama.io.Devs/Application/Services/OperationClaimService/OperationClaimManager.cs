using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.OperationClaimService
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimManager(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task OperationClaimShouldExistsWhenRequested(int operaionClaimId)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(o => o.Id == operaionClaimId);
            if (operationClaim == null) throw new BusinessException("Requested operation claim does not exists.");
        }
    }
}

using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
        }
         
        public async Task UserOperationClaimCanNotDuplicateForSameUserWhenInsert(int userId,int operationClaimId)
        {
            UserOperationClaim? result = await _userOperationClaimRepository.GetAsync(u => u.UserId == userId && u.OperationClaimId == operationClaimId);
            if (result != null) throw new BusinessException("User has this claim already.");
        }
        
        public void UserOperationClaimShouldExistsWhenRequested(UserOperationClaim userOperationClaim)
        {
            if (userOperationClaim == null) throw new BusinessException("Requested user operation claim does not exits.");
        }

        //user veya operation claim kontrolü için buradan repolarını dahil ederek mi yoksa bunların servislerini yazarak mı kullanmalı?
    }
}

using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.UpdateOperationClaim
{
    public class UpdateOperationClaimCommand : IRequest<UpdatedOperationClaimDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateOperaitonClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimDto>
        {
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;
            private readonly IMapper _mapper;
            private readonly IOperationClaimRepository _operationClaimRepository;

            public UpdateOperaitonClaimCommandHandler(OperationClaimBusinessRules operationClaimBusinessRules, IMapper mapper, IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimBusinessRules = operationClaimBusinessRules;
                _mapper = mapper;
                _operationClaimRepository = operationClaimRepository;
            }

            public async Task<UpdatedOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim? tryFindOperationClaimForUpdate = await _operationClaimRepository.GetAsync(o => o.Id == request.Id);

                _operationClaimBusinessRules.OperationClaimShouldExistWhenRequesed(tryFindOperationClaimForUpdate);
                await _operationClaimBusinessRules.OperationClaimNameCanNotDuplicateWhenUpdated(tryFindOperationClaimForUpdate);

                tryFindOperationClaimForUpdate.Name = request.Name;

                OperationClaim updatedOperationClaim = await _operationClaimRepository.UpdateAsync(tryFindOperationClaimForUpdate);
                UpdatedOperationClaimDto updatedOperationClaimDto = _mapper.Map<UpdatedOperationClaimDto>(updatedOperationClaim);

                return updatedOperationClaimDto;
            }
        }
    }
}

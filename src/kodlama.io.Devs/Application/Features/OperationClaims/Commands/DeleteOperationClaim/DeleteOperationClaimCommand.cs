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

namespace Application.Features.OperationClaims.Commands.DeleteOperationClaim
{
    public class DeleteOperationClaimCommand : IRequest<DeletedOperaitonClaimDto>
    {
        public int Id { get; set; }

        public class DeleteOperationClaimHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperaitonClaimDto>
        {
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;
            private readonly IMapper _mapper;
            private readonly IOperationClaimRepository _operationClaimRepository;

            public DeleteOperationClaimHandler(OperationClaimBusinessRules operationClaimBusinessRules, IMapper mapper, IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimBusinessRules = operationClaimBusinessRules;
                _mapper = mapper;
                _operationClaimRepository = operationClaimRepository;
            }

            public async Task<DeletedOperaitonClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim? operationClaimToDelete = await _operationClaimRepository.GetAsync(o => o.Id == request.Id);

                _operationClaimBusinessRules.OperationClaimShouldExistWhenRequesed(operationClaimToDelete);

                OperationClaim deletedOperaitonClaim = await _operationClaimRepository.DeleteAsync(operationClaimToDelete);
                DeletedOperaitonClaimDto deletedOperaitonClaimDto = _mapper.Map<DeletedOperaitonClaimDto>(deletedOperaitonClaim);

                return deletedOperaitonClaimDto;
            }
        }
    }
}

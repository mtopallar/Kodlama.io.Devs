using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.OperationClaimService;
using Application.Services.Repositories;
using Application.Services.UserService;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommand:IRequest<CreatedUserOperationClaimDto>
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimDto>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;
            private readonly IUserService _userService;
            private readonly IOperationClaimService _operationClaimService;

            public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules, IUserService userService, IOperationClaimService operationClaimService)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
                _userService = userService;
                _operationClaimService = operationClaimService;
            }

            public async Task<CreatedUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                
                await _userService.UserShouldExistWhenRequested(request.UserId);
                await _operationClaimService.OperationClaimShouldExistsWhenRequested(request.OperationClaimId);
                await _userOperationClaimBusinessRules.UserOperationClaimCanNotDuplicateForSameUserWhenInsert(request.UserId, request.OperationClaimId);

                UserOperationClaim mapppedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim createdUserOperationClaim = await _userOperationClaimRepository.AddAsync(mapppedUserOperationClaim);

                var model = await _userOperationClaimRepository.GetListAsync(u => u.Id == createdUserOperationClaim.Id,
                    include: u => u.Include(u => u.User).Include(u => u.OperationClaim)); //data returns list
                                                                                          
                var result = model.Items.Single(); //return data to single

                CreatedUserOperationClaimDto createdUserOperationClaimDto = _mapper.Map<CreatedUserOperationClaimDto>(result);

                return createdUserOperationClaimDto;
            }
        }
    }
}

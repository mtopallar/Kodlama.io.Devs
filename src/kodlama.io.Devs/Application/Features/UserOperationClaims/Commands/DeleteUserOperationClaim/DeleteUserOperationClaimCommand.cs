using Application.Features.OperationClaims.Dtos;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimCommand : IRequest<DeletedUserOperationClaimDto>
    {
        public int Id { get; set; }

        public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeletedUserOperationClaimDto>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public DeleteUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<DeletedUserOperationClaimDto> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                UserOperationClaim? tryGetUserOperationClaimForDelete = await _userOperationClaimRepository.GetAsync(u => u.Id == request.Id);
                _userOperationClaimBusinessRules.UserOperationClaimShouldExistsWhenRequested(tryGetUserOperationClaimForDelete);

                var model = await _userOperationClaimRepository.GetListAsync(u => u.Id == request.Id,
                    include: u => u.Include(u => u.User).Include(u => u.OperationClaim)); //data returns list

                var result = model.Items.Single(); //return data to single

                await _userOperationClaimRepository.DeleteAsync(tryGetUserOperationClaimForDelete);

                DeletedUserOperationClaimDto deletedUserOperationClaimDto = _mapper.Map<DeletedUserOperationClaimDto>(result);

                return deletedUserOperationClaimDto;
            }
        }

    }
}

using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Queries.GetListByUserIdUserOperationClaim
{
    public class GetListByUserIdUserOperationClaimQuery : IRequest<UserOperationClaimListModel>,ISecuredRequest    
    {
        public int UserId { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles => new[] { "admin" };

        public class GetListByUserIdUserOperationClaimQueryHandler : IRequestHandler<GetListByUserIdUserOperationClaimQuery, UserOperationClaimListModel>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;            

            public GetListByUserIdUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository)
            {
                _userOperationClaimRepository = userOperationClaimRepository;                
            }

            public async Task<UserOperationClaimListModel> Handle(GetListByUserIdUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == request.UserId,include: u => u.Include(u => u.User).Include(u => u.OperationClaim),index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                List<UserOperationClaimListDto> operationClaimsList = new();
                foreach (var item in userOperationClaims.Items)
                {
                    var dto = new UserOperationClaimListDto
                    {
                        Id = item.Id,
                        OperationClaimName = item.OperationClaim.Name
                    };
                    operationClaimsList.Add(dto);
                }

                var singleModel = userOperationClaims.Items.First();
                UserOperationClaimListModel mappedModel = new()
                {
                    UserFirstName = singleModel.User.FirstName,
                    UserLastName = singleModel.User.LastName,
                    Items = operationClaimsList
                };

                mappedModel.HasNext = userOperationClaims.HasNext;
                mappedModel.HasPrevious = userOperationClaims.HasPrevious;
                mappedModel.Index = userOperationClaims.Index;
                mappedModel.Pages = userOperationClaims.Pages;
                mappedModel.Size = userOperationClaims.Size;
                mappedModel.Count = userOperationClaims.Count;

                return mappedModel;

            }
        }
    }
}

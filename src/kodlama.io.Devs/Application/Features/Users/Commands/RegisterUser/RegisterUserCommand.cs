using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<AccessToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AccessToken>
        {
            private readonly ITokenHelper _tokenHelper;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IUserRepository _userRepository;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly IMapper _mapper;

            public RegisterUserCommandHandler(ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository, IUserRepository userRepository, UserBusinessRules userBusinessRoles, IMapper mapper)
            {
                _tokenHelper = tokenHelper;
                _userOperationClaimRepository = userOperationClaimRepository;
                _userRepository = userRepository;
                _userBusinessRules = userBusinessRoles;
                _mapper = mapper;
            }

            public async Task<AccessToken> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.CanNotDuplicateEmailWhenInserted(request.Email);

                HashingHelper.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

                User mappedUser = _mapper.Map<User>(request);
                mappedUser.PasswordSalt = passwordSalt;
                mappedUser.PasswordHash = passwordHash;
                mappedUser.Status = true;

                User createdUser = await _userRepository.AddAsync(mappedUser);

                IPaginate<UserOperationClaim> usersOperationClaims = await _userOperationClaimRepository.GetListAsync(
                    u => u.UserId == createdUser.Id,
                    include: u => u.Include(o => o.OperationClaim),
                    cancellationToken: cancellationToken
                    );

                AccessToken createdAccessToken = _tokenHelper.CreateToken(createdUser, usersOperationClaims.Items.Select(u => u.OperationClaim).ToList());

                return createdAccessToken;
            }
        }
    }
}

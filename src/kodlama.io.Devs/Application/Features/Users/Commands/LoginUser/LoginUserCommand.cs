using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<AccessToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _userBusinessRules;

            public LoginUserCommandHandler(IUserRepository userRepository, IUserOperationClaimRepository userOperationClaimRepository, ITokenHelper tokenHelper, UserBusinessRules userBusinessRoles)
            {
                _userRepository = userRepository;
                _userOperationClaimRepository = userOperationClaimRepository;
                _tokenHelper = tokenHelper;
                _userBusinessRules = userBusinessRoles;
            }

            public async Task<AccessToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                User? tryGetUserByMailForLogin = await _userRepository.GetAsync(u => u.Email == request.Email);
                _userBusinessRules.UserExistsWhenRequested(tryGetUserByMailForLogin);
                _userBusinessRules.CheckUserPassword(request.Password, tryGetUserByMailForLogin.PasswordHash, tryGetUserByMailForLogin.PasswordSalt);

               IPaginate<UserOperationClaim> usersOperationClaim = await _userOperationClaimRepository.GetListAsync(
                     u => u.UserId == tryGetUserByMailForLogin.Id,
                     include: u => u.Include(o => o.OperationClaim),
                     cancellationToken: cancellationToken
                     );

                AccessToken createdToken = _tokenHelper.CreateToken(tryGetUserByMailForLogin, usersOperationClaim.Items.Select(o=>o.OperationClaim).ToList());

                return createdToken;
            }
        }
    }
}

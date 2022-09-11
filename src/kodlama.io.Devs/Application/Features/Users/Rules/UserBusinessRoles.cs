using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CanNotDuplicateEmailWhenInserted(string email)
        {
           IPaginate<User> result = await _userRepository.GetListAsync(u => u.Email == email);
            if (result.Items.Any()) throw new BusinessException("This email used already.");
        }

        public void UserExistsWhenRequested(User user)
        {
            if (user == null) throw new BusinessException("Requested user not exists.");
        }

        public void CheckUserPassword(string password, byte[] paswordHash, byte[] passwordSalt)
        {
            if(!HashingHelper.VerifyPasswordHash(password, paswordHash, passwordSalt)) throw new AuthorizationException("Wrong password.");
        }
    }
}

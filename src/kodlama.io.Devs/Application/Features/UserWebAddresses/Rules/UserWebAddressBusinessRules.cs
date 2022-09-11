using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserWebAddresses.Rules
{
    public class UserWebAddressBusinessRules
    {
        private readonly IUserWebAddressRepository _userWebAddressRepository;

        public UserWebAddressBusinessRules(IUserWebAddressRepository userWebAddressRepository)
        {
            _userWebAddressRepository = userWebAddressRepository;
        }

        public async Task CanNotDuplicateAdressesForSameUserWhenInserted(int userId)
        {
            IPaginate<UserWebAddress> tryFindUserHasAdressAlready = await _userWebAddressRepository.GetListAsync(u => u.UserId == userId);
            if (tryFindUserHasAdressAlready.Items.Any()) throw new BusinessException("User has already added the address before.");
        }

        public async Task CanNotDuplicateAdressesForSameUserWhenUpdate(UserWebAddress userWebAddress)
        {
            IPaginate<UserWebAddress> tryFindUserHasAdressAlready = await _userWebAddressRepository.GetListAsync(u => u.UserId == userWebAddress.UserId && u.Id != userWebAddress.Id);
            if (tryFindUserHasAdressAlready.Items.Any()) throw new BusinessException("The data you're trying to update exists.");
        }

        public void UserWebAddressExistWhenRequested(UserWebAddress userWebAddress)
        {
            if (userWebAddress == null) throw new BusinessException("Requested user web address doesn't exists.");
        }
    }
}

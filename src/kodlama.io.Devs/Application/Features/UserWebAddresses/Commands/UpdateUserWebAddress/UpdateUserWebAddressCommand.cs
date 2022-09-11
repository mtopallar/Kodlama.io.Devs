using Application.Features.UserWebAddresses.Dtos;
using Application.Features.UserWebAddresses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserWebAddresses.Commands.UpdateUserWebAddress
{
    public class UpdateUserWebAddressCommand : IRequest<UpdatedUserWebAdressDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GithubAddress { get; set; }

        public class UpdateUserWebAddressCommandHandler : IRequestHandler<UpdateUserWebAddressCommand, UpdatedUserWebAdressDto>
        {
            private readonly IUserWebAddressRepository _userWebAddressRepository;
            private IMapper _mapper;
            private readonly UserWebAddressBusinessRules _userWebAddressBusinessRules;

            public UpdateUserWebAddressCommandHandler(IUserWebAddressRepository userWebAddressRepository, IMapper mapper, UserWebAddressBusinessRules userWebAddressBusinessRules)
            {
                _userWebAddressRepository = userWebAddressRepository;
                _mapper = mapper;
                _userWebAddressBusinessRules = userWebAddressBusinessRules;
            }

            public async Task<UpdatedUserWebAdressDto> Handle(UpdateUserWebAddressCommand request, CancellationToken cancellationToken)
            {
                UserWebAddress? tryGetExistingAdressForUpdate = await _userWebAddressRepository.GetAsync(u => u.Id == request.Id);

                _userWebAddressBusinessRules.UserWebAddressExistWhenRequested(tryGetExistingAdressForUpdate);

                tryGetExistingAdressForUpdate.UserId = request.UserId;
                tryGetExistingAdressForUpdate.GithubAddress = request.GithubAddress;

                await _userWebAddressBusinessRules.CanNotDuplicateAdressesForSameUserWhenUpdate(tryGetExistingAdressForUpdate);

                UserWebAddress updatedUserWebAddress = await _userWebAddressRepository.UpdateAsync(tryGetExistingAdressForUpdate);
                UpdatedUserWebAdressDto mappedUserWebAddressDto = _mapper.Map<UpdatedUserWebAdressDto>(updatedUserWebAddress);

                return mappedUserWebAddressDto;
            }
        }
    }
}

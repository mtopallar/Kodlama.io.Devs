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

namespace Application.Features.UserWebAddresses.Commands.CreateUserWebAddress
{
    public class CreateUserWebAddressCommand : IRequest<CreatedUserWebAddressDto>
    {
        public int UserId { get; set; }
        public string GithubAddress { get; set; }

        public class CreateUserWebAddressCommandHandler : IRequestHandler<CreateUserWebAddressCommand, CreatedUserWebAddressDto>
        {
            private readonly IUserWebAddressRepository _userWebAddressRepository;
            private readonly IMapper _mapper;
            private readonly UserWebAddressBusinessRules _userWebAddressBusinessRules;

            public CreateUserWebAddressCommandHandler(IUserWebAddressRepository userWebAddressRepository, IMapper mapper, UserWebAddressBusinessRules userWebAddressBusinessRules)
            {
                _userWebAddressRepository = userWebAddressRepository;
                _mapper = mapper;
                _userWebAddressBusinessRules = userWebAddressBusinessRules;
            }

            public async Task<CreatedUserWebAddressDto> Handle(CreateUserWebAddressCommand request, CancellationToken cancellationToken)
            {
                await _userWebAddressBusinessRules.CanNotDuplicateAdressesForSameUserWhenInserted(request.UserId);

                UserWebAddress mappedUserWebAddress = _mapper.Map<UserWebAddress>(request);
                UserWebAddress createdUserWebAddress = await _userWebAddressRepository.AddAsync(mappedUserWebAddress);
                CreatedUserWebAddressDto createdUserWebAddressDto = _mapper.Map<CreatedUserWebAddressDto>(createdUserWebAddress);

                return createdUserWebAddressDto;
            }
        }
    }
}

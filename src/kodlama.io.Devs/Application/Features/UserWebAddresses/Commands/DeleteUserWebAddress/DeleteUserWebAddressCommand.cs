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

namespace Application.Features.UserWebAddresses.Commands.DeleteUserWebAddress
{
    public class DeleteUserWebAddressCommand : IRequest<DeletedUserWebAddressDto>
    {
        public int Id { get; set; }

        public class DeleteUserWebAddressCommandHandler : IRequestHandler<DeleteUserWebAddressCommand, DeletedUserWebAddressDto>
        {
            private readonly IUserWebAddressRepository _userWebAddressRepository;
            private readonly IMapper _mapper;
            private readonly UserWebAddressBusinessRules _userWebAddressBusinessRules;

            public DeleteUserWebAddressCommandHandler(IUserWebAddressRepository userWebAddressRepository, IMapper mapper, UserWebAddressBusinessRules userWebAddressBusinessRules)
            {
                _userWebAddressRepository = userWebAddressRepository;
                _mapper = mapper;
                _userWebAddressBusinessRules = userWebAddressBusinessRules;
            }

            public async Task<DeletedUserWebAddressDto> Handle(DeleteUserWebAddressCommand request, CancellationToken cancellationToken)
            {
                UserWebAddress? tryFindUserWebAddressForDelete = await _userWebAddressRepository.GetAsync(u => u.Id == request.Id);

                _userWebAddressBusinessRules.UserWebAddressExistWhenRequested(tryFindUserWebAddressForDelete);

                UserWebAddress deletedUserWebAddress = await _userWebAddressRepository.DeleteAsync(tryFindUserWebAddressForDelete);
                DeletedUserWebAddressDto mappedCreatedUserWebAddressDto = _mapper.Map<DeletedUserWebAddressDto>(deletedUserWebAddress);

                return mappedCreatedUserWebAddressDto;
            }
        }
    }
}

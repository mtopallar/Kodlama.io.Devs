using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserWebAddresses.Commands.CreateUserWebAddress
{
    public class CreateUserWebAddressCommandValidator : AbstractValidator<CreateUserWebAddressCommand>
    {
        public CreateUserWebAddressCommandValidator()
        {
            RuleFor(u => u.UserId).NotEqual(0);
            RuleFor(u => u.GithubAddress).NotEmpty();
        }
    }
}

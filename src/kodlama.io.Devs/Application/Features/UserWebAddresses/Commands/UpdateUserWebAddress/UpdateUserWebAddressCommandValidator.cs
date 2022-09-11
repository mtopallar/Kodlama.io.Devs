using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserWebAddresses.Commands.UpdateUserWebAddress
{
    public class UpdateUserWebAddressCommandValidator : AbstractValidator<UpdateUserWebAddressCommand>
    {
        public UpdateUserWebAddressCommandValidator()
        {
            RuleFor(u => u.UserId).NotEqual(0);
            RuleFor(u => u.GithubAddress).NotEmpty();
        }
    }
}

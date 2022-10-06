using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(r=>r.UserForRegisterDto.FirstName).NotEmpty();
            RuleFor(r=>r.UserForRegisterDto.LastName).NotEmpty();
            RuleFor(r=>r.UserForRegisterDto.Email).NotEmpty();
            RuleFor(r => r.UserForRegisterDto.Password).NotEmpty();
        }
    }
}

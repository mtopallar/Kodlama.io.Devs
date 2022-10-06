using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(l=>l.UserForLoginDto.Email).NotEmpty();
            RuleFor(l=>l.UserForLoginDto.Password).NotEmpty();            
        }
    }
}

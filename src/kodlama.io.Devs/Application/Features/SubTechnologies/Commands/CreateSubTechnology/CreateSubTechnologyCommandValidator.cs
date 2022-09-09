using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SubTechnologies.Commands.CreateSubTechnology
{
    public class CreateSubTechnologyCommandValidator : AbstractValidator<CreateSubTechnologyCommand>
    {
        public CreateSubTechnologyCommandValidator()
        {
            RuleFor(s => s.ProgrammingLanguageId).NotEqual(0);
            RuleFor(s => s.Name).NotEmpty();
        }
    }
}

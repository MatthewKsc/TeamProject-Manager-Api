using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TeamProject_Manager_Api.Dtos.Models_Operations;

namespace TeamProject_Manager_Api.Dtos.Validators
{
    public class TeamValidator : AbstractValidator<CreateTeam>{

        public TeamValidator() {
            RuleFor(x => x.NameOfTeam)
                .NotEmpty()
                .WithMessage("Name of a team cannot be empty")
                .Length(5, 255)
                .WithMessage("Name of a team need to be in Range of characters from 5 to 255");
        }
    }
}

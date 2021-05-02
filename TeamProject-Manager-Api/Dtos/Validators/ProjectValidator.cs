using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TeamProject_Manager_Api.Dtos.Models_Operations;

namespace TeamProject_Manager_Api.Dtos.Validators
{
    public class ProjectValidator : AbstractValidator<CreateProject>{

        public ProjectValidator() {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title of a project cannot be empty")
                .Length(10, 100)
                .WithMessage("Title lenght need to be in Range of characters from 10 to 100");

            When(x => x.StartOfProject != null, () => {
                RuleFor(x=> x.StartOfProject)
                    .Must(value => value.Value >= DateTime.Now.Date)
                    .WithMessage($"Start of a project can't be set before actual date: {DateTime.Now.ToString("MM/dd/yyyy")}");
            }).Otherwise(()=> {
                RuleFor(x => x.StartOfProject)
                    .NotEmpty()
                    .WithMessage("Start of a project need to be set");
            });
        }
    }
}

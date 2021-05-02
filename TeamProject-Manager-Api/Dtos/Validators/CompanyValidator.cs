using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using TeamProject_Manager_Api.Dtos.Models_Operations;

namespace TeamProject_Manager_Api.Dtos.Validators
{
    public class CompanyValidator : AbstractValidator<CreateCompany>{

        public CompanyValidator() {

            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Company name cannot be empty")
                .Length(2, 100)
                .WithMessage("Comapny name need to be in Rage of characters from 2 to 100");

            RuleFor(x => x.SizeOfComapny)
                .NotEmpty().WithMessage("Size of company need to be set");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country name cannot be empty")
                .Length(1, 60)
                .WithMessage("Country name need to be in Rage of characters from 1 to 60");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City name cannot be empty")
                .Length(1, 85)
                .WithMessage("City name need to be in Rage of characters from 1 to 85");

            RuleFor(x => x.Street)
                .NotEmpty()
                .WithMessage("Street name cannot be empty")
                .Length(1, 100)
                .WithMessage("Street name need to be in Rage of characters from 1 to 100");

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .WithMessage("PostalCode name cannot be empty")
                .Must(value => Regex.IsMatch(value, @"\d{2}-\d{3}"))
                .WithMessage("Not a valid PostalCode format");
        }
    }
}

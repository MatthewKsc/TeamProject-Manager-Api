using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.Dtos.Models_Operations;

namespace TeamProject_Manager_Api.Dtos.Validators
{
    public class UserValidator : AbstractValidator<CreateUser>{

        public UserValidator(ProjectManagerDbContext dbcontext) {

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName of user cannot be empty");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName of user cannot be empty")
                .Custom((value, context) => {
                    var userWithSameLastName = dbcontext.Users.Any(u => u.LastName.ToLower().Equals(value.ToLower()));
                    if (userWithSameLastName) {
                        context.AddFailure("LastName", "Already user with same lastname for email uniqnes please provide lastname with random number at end for example : Simson1");
                    }
                });

            When(x => x.DateOfBirth != null, () => {
                RuleFor(x => x.DateOfBirth)
                    .Must(value => DateTime.Now.Year-value.Value.Year >= 16)
                    .WithMessage("Person need to be at least 16 years old");
            }).Otherwise(() => {
                 RuleFor(x => x.DateOfBirth)
                    .NotEmpty()
                    .WithMessage("Person date of birth need to be provided");
            });

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country name cannot be empty")
                .Length(1, 60)
                .WithMessage("Country name need to be in Rage of characters from 1 to 60");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City name cannot be empty")
                .Length(1, 85)
                .WithMessage("City name need to be in Rage of characters from 1 to 85");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street name cannot be empty")
                .Length(1, 100)
                .WithMessage("Street name need to be in Rage of characters from 1 to 100");

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("PostalCode name cannot be empty")
                .Must(value => Regex.IsMatch(value, @"\d{2}-\d{3}"))
                .WithMessage("Not a valid PostalCode format");
        }

    }
}

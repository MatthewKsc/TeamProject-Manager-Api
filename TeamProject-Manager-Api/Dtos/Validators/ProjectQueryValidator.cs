using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Dtos.Querying_Models;

namespace TeamProject_Manager_Api.Dtos.Validators
{
    public class ProjectQueryValidator : AbstractValidator<Query<ProjectDTO>>{

        private int[] allowedPageSize = new int[] { 5, 10, 15 };
        private string allowedSortByColumnName =  nameof(Project.Title);

        public ProjectQueryValidator() {

            RuleFor(x => x.pageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page number need to be selected at least with 1 or grater number");

            RuleFor(x => x.pageSize)
                .Custom((value, context) => {
                    if (!allowedPageSize.Contains(value)) {
                        context.AddFailure("Page Size", $"Page Size need to be in format [{string.Join(',', allowedPageSize)}]");
                    }
                });

            RuleFor(u => u.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnName.Equals(value))
                .WithMessage("Sort by need to be null or by Title");
        }

    }
}

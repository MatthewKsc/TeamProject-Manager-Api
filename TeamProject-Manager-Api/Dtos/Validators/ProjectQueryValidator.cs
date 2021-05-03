using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Dtos.Querying_Models;

namespace TeamProject_Manager_Api.Dtos.Validators
{
    public class ProjectQueryValidator : AbstractValidator<QueryResult<ProjectDTO>>{
        
    }
}

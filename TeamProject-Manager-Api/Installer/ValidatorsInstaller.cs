using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Dtos.Models_Operations;
using TeamProject_Manager_Api.Dtos.Querying_Models;
using TeamProject_Manager_Api.Dtos.Validators;

namespace TeamProject_Manager_Api.Installer
{
    public class ValidatorsInstaller : IInstaller {
        public void InstallServices(IServiceCollection services, IConfiguration configuration) {

            services.AddScoped<IValidator<CreateTeam>, TeamValidator>();
            services.AddScoped<IValidator<CreateProject>, ProjectValidator>();
            services.AddScoped<IValidator<CreateCompany>, CompanyValidator>();
            services.AddScoped<IValidator<CreateUser>, UserValidator>();
            services.AddScoped<IValidator<Query<UserDTO>>, UserQueryValidator>();
            services.AddScoped<IValidator<Query<ProjectDTO>>, ProjectQueryValidator>();
        }
    }
}

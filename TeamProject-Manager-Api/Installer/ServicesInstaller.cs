using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.Services;

namespace TeamProject_Manager_Api.Installer
{
    public class ServicesInstaller : IInstaller {
        public void InstallServices(IServiceCollection services, IConfiguration configuration) {

            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserProjectsService, UserProjectsService>();

        }
    }
}

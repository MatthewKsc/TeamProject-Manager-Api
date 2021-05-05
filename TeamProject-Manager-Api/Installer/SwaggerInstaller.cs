using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.Installer
{
    public class SwaggerInstaller : IInstaller {
        public void InstallServices(IServiceCollection services, IConfiguration configuration) {

            services.AddSwaggerGen(s => {
                s.EnableAnnotations();
                s.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "Project Manager API",
                    Description = "API create in ASP.NET Core Web for managing project within a team/company. Project is one of my Github projects to give a sight at what can i do :) ",
                    Contact = new OpenApiContact {
                        Name = "Mateusz Ksciuk",
                        Email = "ksciukmateusz@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/mateusz-ksciuk-7a0b69197/?locale=en_US")
                    },
                    License = new OpenApiLicense {
                        Name = "Github Repo",
                        Url = new Uri("https://github.com/MatthewKsc/TeamProject-Manager-Api")
                    }
                });

            });

        }
    }
}

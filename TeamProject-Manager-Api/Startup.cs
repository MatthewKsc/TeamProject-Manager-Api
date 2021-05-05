using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Dtos.Models_Operations;
using TeamProject_Manager_Api.Dtos.Querying_Models;
using TeamProject_Manager_Api.Dtos.Validators;
using TeamProject_Manager_Api.Exceptions;
using TeamProject_Manager_Api.Services;

namespace TeamProject_Manager_Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddControllers().AddFluentValidation();
            services.AddAutoMapper(this.GetType().Assembly);

            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserProjectsService, UserProjectsService>();

            services.AddScoped<IValidator<CreateTeam>, TeamValidator>();
            services.AddScoped<IValidator<CreateProject>, ProjectValidator>();
            services.AddScoped<IValidator<CreateCompany>, CompanyValidator>();
            services.AddScoped<IValidator<CreateUser>, UserValidator>();
            services.AddScoped<IValidator<Query<UserDTO>>, UserQueryValidator>();
            services.AddScoped<IValidator<Query<ProjectDTO>>, ProjectQueryValidator>();


            services.AddScoped<ErrorHandlingMiddleware>();

            services.AddSwaggerGen();

            services.AddDbContext<ProjectManagerDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection"))
            );

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddScoped<ProjectManagerSeeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProjectManagerSeeder seeder) {

            //to fill up db at ealry start of program
            seeder.Seed();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(s => {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Project-Manager API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}

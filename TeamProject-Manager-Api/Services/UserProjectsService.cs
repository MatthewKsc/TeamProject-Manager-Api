using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Exceptions;

namespace TeamProject_Manager_Api.Services
{

    public interface IUserProjectsService {

        void AddUserToProject(string userEmail, int projectId);
        void AddUserToProject(List<string> userEmail, int projectId);
    }

    public class UserProjectsService: IUserProjectsService{

        private readonly ProjectManagerDbContext context;

        public UserProjectsService(ProjectManagerDbContext context) {
            this.context = context;
        }

        public void AddUserToProject(string userEmail, int projectId) {

            Project project = context.Projects
                .SingleOrDefault(p => p.Id == projectId);

            if (project is null)
                throw new NotFoundException($"There is no such project with id: {projectId}");

            User user = 
                context.Users.SingleOrDefault(u => u.Email.ToLower().Equals(userEmail.ToLower()));

            if (user is null)
                throw new NotFoundException($"There is no such user with Email: {userEmail}");

            UserProject userProject = new UserProject(user, project);

            context.UserProjects.Add(userProject);
            context.SaveChanges();
        }

        public void AddUserToProject(List<string> userEmail, int projectId) {
            Project project = context.Projects
                .SingleOrDefault(p => p.Id == projectId);

            if (project is null)
                throw new NotFoundException($"There is no such project with id: {projectId}");

            List<User> users = context.Users
                .Where(u => userEmail.Contains(u.Email)).ToList();

            if (userEmail.Count != users.Count || users.Count < 1)
                throw new NotFoundException("One or more users are not valid please check Emails and Project");

            List<UserProject> userProjects = UserProject.AddManyUsersToProject(users, project);

            context.UserProjects.AddRange(userProjects);
            context.SaveChanges();
        }
    }
}

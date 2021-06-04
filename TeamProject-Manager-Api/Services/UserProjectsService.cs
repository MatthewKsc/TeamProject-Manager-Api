using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Exceptions;
using TeamProject_Manager_Api.Repositories;

namespace TeamProject_Manager_Api.Services
{

    public interface IUserProjectsService {

        void AddUserToProject(string userEmail, int projectId);
        void AddUserToProject(List<string> userEmail, int projectId);
        void RemoveUserFromProject(string userEmail, int projectId);
    }

    public class UserProjectsService: IUserProjectsService{

        private readonly IUserProjectsRepository userProjectsRepository;
        private readonly IProjectRepository projectRepository;
        private readonly IUserRepository userRepository;

        public UserProjectsService(IUserProjectsRepository userProjectsRepository, 
                    IProjectRepository projectRepository, 
                    IUserRepository userRepository) {
            this.userProjectsRepository = userProjectsRepository;
            this.projectRepository = projectRepository;
            this.userRepository = userRepository;
        }

        public void AddUserToProject(string userEmail, int projectId) {

            Project project = projectRepository.GetProjectById(projectId);

            if (project is null)
                throw new NotFoundException($"There is no such project with id: {projectId}");

            User user = userRepository.GetUserByEmail(userEmail);

            if (user is null)
                throw new NotFoundException($"There is no such user with Email: {userEmail}");

            UserProject userProject = new UserProject(user, project);

            userProjectsRepository.AddUserProject(userProject);
        }

        public void AddUserToProject(List<string> userEmail, int projectId) {

            Project project = projectRepository.GetProjectById(projectId);

            if (project is null)
                throw new NotFoundException($"There is no such project with id: {projectId}");

            List<User> users = userRepository.GetUserByEmail(userEmail);

            if (userEmail.Count != users.Count || users.Count < 1)
                throw new NotFoundException("One or more users are not valid please check Emails and Project");

            List<UserProject> userProjects = UserProject.AddManyUsersToProject(users, project);

            userProjectsRepository.AddUserProject(userProjects);
        }

        public void RemoveUserFromProject(string userEmail, int projectId) {

            Project project = projectRepository.GetProjectById(projectId);

            if (project is null)
                throw new NotFoundException($"There is no such project with id: {projectId}");

            User user = userRepository.GetUserByEmail(userEmail);

            if (user is null)
                throw new NotFoundException($"There is no such user with Email: {userEmail}");

            UserProject userProject = userProjectsRepository.GetUserProject(project.Id, user.Id);

            if (userProject is null)
                throw new NotFoundException("This user is not assigned to this project !");

            userProjectsRepository.DeleteUserProject(userProject);
        }
    }
}

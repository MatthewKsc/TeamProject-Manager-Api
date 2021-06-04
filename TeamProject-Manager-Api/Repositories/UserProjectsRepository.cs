using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;

namespace TeamProject_Manager_Api.Repositories
{
    public interface IUserProjectsRepository {

        UserProject GetUserProject(int projectId, int userId);
        void AddUserProject(UserProject userProject);
        void AddUserProject(List<UserProject> userProjects);
        void DeleteUserProject(UserProject userProject);
    }

    public class UserProjectsRepository : IUserProjectsRepository {

        private readonly ProjectManagerDbContext context;

        public UserProjectsRepository(ProjectManagerDbContext context) {
            this.context = context;
        }

        public UserProject GetUserProject(int projectId, int userId) {
            return context.UserProjects
                .SingleOrDefault(up => up.ProjectId == projectId && up.UserId == userId);
        }

        public void AddUserProject(UserProject userProject) {
            context.UserProjects.Add(userProject);
            context.SaveChanges();
        }

        public void AddUserProject(List<UserProject> userProjects) {
            context.UserProjects.AddRange(userProjects);
            context.SaveChanges();
        }

        public void DeleteUserProject(UserProject userProject) {
            context.UserProjects.Remove(userProject);
            context.SaveChanges();
        }
    }
}

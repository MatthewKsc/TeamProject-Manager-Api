using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;

namespace TeamProject_Manager_Api.Services{
    public interface IProjectService {

        List<Project> GetAllProjects(int teamId);
        Project GetProjectById(int teamId, int Id);
        void CreateProject(Project project, int teamId);
        bool DeleteProjectById(int teamId, int Id);

    }

    public class ProjectService : IProjectService{
        
        private readonly ProjectManagerDbContext context;

        public ProjectService(ProjectManagerDbContext context) {
            this.context = context;
        }

        public List<Project> GetAllProjects(int teamId) {
            List<Project> projest = context.Projects
                .Where(p => p.OwnerTeamId == teamId)
                .Include(t => t.UsersAssigned)
                .Include(t => t.OwnerTeam)
                .ToList();

            return projest;
        }

        public Project GetProjectById(int teamId, int Id) {
            Project project = context
                .Projects.SingleOrDefault(p => p.OwnerTeamId == teamId && p.Id == Id);

            return project;
        }

        public void CreateProject(Project project, int teamId) {
            project.OwnerTeamId = teamId;

            context.Projects.Add(project);
            context.SaveChanges();
        }

        public bool DeleteProjectById(int teamId, int Id) {
            Project project = context
                .Projects.SingleOrDefault(p => p.OwnerTeamId == teamId && p.Id == Id);

            if (project is null)
                return false;

            context.Projects.Remove(project);
            context.SaveChanges();

            return true;
        }    
    }
}

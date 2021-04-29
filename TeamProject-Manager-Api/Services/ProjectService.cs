using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Exceptions;

namespace TeamProject_Manager_Api.Services{
    public interface IProjectService {

        List<Project> GetAllProjects(int teamId);
        Project GetProjectById(int teamId, int Id);
        void CreateProject(Project project, int teamId);
        void DeleteProjectById(int teamId, int Id);

    }

    public class ProjectService : IProjectService{
        
        private readonly ProjectManagerDbContext context;

        public ProjectService(ProjectManagerDbContext context) {
            this.context = context;
        }

        public List<Project> GetAllProjects(int teamId) {

            ValidTeam(teamId);

            List<Project> projects = context.Projects
                .Where(p => p.OwnerTeamId == teamId)
                .Include(t => t.OwnerTeam)
                .ToList();

            if (projects.Count < 1)
                throw new NotFoundException("There is no projects to display");

            return projects;
        }

        public Project GetProjectById(int teamId, int Id) {

            ValidTeam(teamId);

            Project project = context
                .Projects.SingleOrDefault(p => p.OwnerTeamId == teamId && p.Id == Id);

            if (project is null)
                throw new NotFoundException($"There is no project with id: {Id}");

            return project;
        }

        public void CreateProject(Project project, int teamId) {

            ValidTeam(teamId);

            project.OwnerTeamId = teamId;

            context.Projects.Add(project);
            context.SaveChanges();
        }

        public void DeleteProjectById(int teamId, int Id) {

            ValidTeam(teamId);

            Project project = context
                .Projects.SingleOrDefault(p => p.OwnerTeamId == teamId && p.Id == Id);

            if (project is null)
                throw new NotFoundException($"There is no project with id: {Id}");

            context.Projects.Remove(project);
            context.SaveChanges();
        }

        private void ValidTeam(int teamId) {
            if (!context.Teams.Any(t => t.Id == teamId))
                throw new BadRequestException($"There is no team with id {teamId}");
        }
    }
}

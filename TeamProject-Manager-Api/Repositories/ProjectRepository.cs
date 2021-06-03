using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Dtos.Querying_Models;

namespace TeamProject_Manager_Api.Repositories
{

    public interface IProjectRepository {
        IQueryable<Project> GetProjectQuery(Query<ProjectDTO> query, int teamId);
        List<Project> GetProjectsWithQuery(Query<ProjectDTO> query, IQueryable<Project> baseQuery);
        List<Project> GetProjects(int teamId);
        Project GetProjectById(int Id);
        Project GetProjectByIdWithIncludes(int Id);
        void CreateProject(Project project);
        void UpdateProject(Project updatedProject);
        void DeleteProject(Project project);
    }

    public class ProjectRepository : IProjectRepository {

        private readonly ProjectManagerDbContext context;

        public ProjectRepository(ProjectManagerDbContext context) {
            this.context = context;
        }

        public Project GetProjectById(int Id) {
            return context.Projects
                .SingleOrDefault(p => p.Id == Id);
        }

        public Project GetProjectByIdWithIncludes(int Id) {
            return context.Projects
                .Include(t => t.OwnerTeam)
                .Include(p => p.UserProjects)
                    .ThenInclude(up => up.User)
                .SingleOrDefault(p => p.Id == Id);
        }

        public List<Project> GetProjects(int teamId) {
            return context.Projects
                .Where(p => p.OwnerTeamId == teamId)
                .Include(t => t.OwnerTeam)
                .Include(p => p.UserProjects)
                    .ThenInclude(up => up.User)
                .ToList();
        }

        public IQueryable<Project> GetProjectQuery(Query<ProjectDTO> query, int teamId) {
            return context.Projects
                .Where(p => p.OwnerTeamId == teamId)
                .Include(t => t.OwnerTeam)
                .Include(p => p.UserProjects)
                    .ThenInclude(up => up.User)
                .Where(p => query.searchPhrase == null || string.IsNullOrEmpty(query.searchPhrase) ||
                    (
                        p.Description.ToLower().Equals(query.searchPhrase.ToLower()) ||
                        p.Title.ToLower().Equals(query.searchPhrase.ToLower())
                    )
                );
        }

        public List<Project> GetProjectsWithQuery(Query<ProjectDTO> query, IQueryable<Project> baseQuery) {
            return baseQuery
                .Skip(query.pageSize * (query.pageNumber - 1))
                .Take(query.pageSize)
                .ToList();
        }

        public void CreateProject(Project project) {
            context.Projects.Add(project);
            context.SaveChanges();
        }

        public void UpdateProject(Project updatedProject) {
            context.Update(updatedProject);
            context.SaveChanges();
        }

        public void DeleteProject(Project project) {
            context.Projects.Remove(project);
            context.SaveChanges();
        }
    }
}

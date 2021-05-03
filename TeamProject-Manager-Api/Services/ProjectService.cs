using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Dtos.Models_Operations;
using TeamProject_Manager_Api.Dtos.Querying_Models;
using TeamProject_Manager_Api.Exceptions;

namespace TeamProject_Manager_Api.Services{
    public interface IProjectService {

        PageResult<ProjectDTO> GetAllProjects(Query<ProjectDTO> query, int teamId);
        ProjectDTO GetProjectById(int teamId, int Id);
        int CreateProject(CreateProject project, int teamId);
        void UpdateProject(CreateProject updatedProject, int Id);
        void DeleteProjectById(int teamId, int Id);
    }

    public class ProjectService : IProjectService{
        
        private readonly ProjectManagerDbContext context;
        private readonly IMapper mapper;

        public ProjectService(ProjectManagerDbContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }

        public PageResult<ProjectDTO> GetAllProjects(Query<ProjectDTO> query, int teamId) {

            ValidTeam(teamId);

            IQueryable<Project> baseQuery = context.Projects
                .Where(p => p.OwnerTeamId == teamId)
                .Include(t => t.OwnerTeam)
                .Include(p => p.UserProjects)
                    .ThenInclude(up => up.User)
                .Where(p=> query.searchPhrase == null ||
                    (
                        p.Description.ToLower().Equals(query.searchPhrase.ToLower()) ||
                        p.Title.ToLower().Equals(query.searchPhrase.ToLower())
                    )
                );

            if (!string.IsNullOrEmpty(query.SortBy)) {
                baseQuery = query.SortDirection == SortDirection.ASC ? 
                    baseQuery.OrderBy(p => p.Title) : baseQuery.OrderByDescending(p => p.Title);
            }

            List<Project> projects = baseQuery
                .Skip(query.pageSize * (query.pageNumber - 1))
                .Take(query.pageSize)
                .ToList();

            int totalItemsCount = projects.Count;

            var DTOs = mapper.Map<List<ProjectDTO>>(projects);

            var result = new PageResult<ProjectDTO>(DTOs, totalItemsCount, query.pageSize, query.pageNumber);

            return result;
        }

        public ProjectDTO GetProjectById(int teamId, int Id) {

            ValidTeam(teamId);

            Project project = context.Projects
                .Include(t=> t.OwnerTeam)
                .Include(p=> p.UserProjects)
                    .ThenInclude(up=>up.User)
                .SingleOrDefault(p => p.OwnerTeamId == teamId && p.Id == Id);

            if (project is null)
                throw new NotFoundException($"There is no project with id: {Id}");

            var result = mapper.Map<ProjectDTO>(project);

            return result;
        }

        public int CreateProject(CreateProject createProject, int teamId) {

            ValidTeam(teamId);

            Project project = mapper.Map<Project>(createProject);

            project.OwnerTeamId = teamId;

            context.Projects.Add(project);
            context.SaveChanges();

            return project.Id;
        }

        public void UpdateProject(CreateProject updatedProject, int Id) {

            Project project = context.Projects.SingleOrDefault(p => p.Id == Id);

            if (project is null)
                throw new NotFoundException($"There is no project with id: {Id}");

            project = mapper.Map(updatedProject, project);

            context.Update(project);
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

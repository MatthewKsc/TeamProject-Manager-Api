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
using TeamProject_Manager_Api.Repositories;

namespace TeamProject_Manager_Api.Services{
    public interface IProjectService {

        PageResult<ProjectDTO> GetAllProjects(Query<ProjectDTO> query, int teamId);
        ProjectDTO GetProjectById(int Id);
        int CreateProject(CreateProject project, int teamId);
        void UpdateProject(CreateProject updatedProject, int Id);
        void DeleteProjectById(int Id);
    }

    public class ProjectService : IProjectService{

        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;

        public ProjectService(IProjectRepository projectRepository,  IMapper mapper) {
            this.projectRepository = projectRepository;
            this.mapper = mapper;
        }

        public PageResult<ProjectDTO> GetAllProjects(Query<ProjectDTO> query, int teamId) {

            IQueryable<Project> baseQuery = projectRepository.GetProjectQuery(query, teamId);

            if (!string.IsNullOrEmpty(query.SortBy)) {
                baseQuery = query.SortDirection == SortDirection.ASC ? 
                    baseQuery.OrderBy(p => p.Title) : baseQuery.OrderByDescending(p => p.Title);
            }

            List<Project> projects = projectRepository.GetProjectsWithQuery(query, baseQuery);

            int totalItemsCount = projects.Count;

            var DTOs = mapper.Map<List<ProjectDTO>>(projects);

            var result = new PageResult<ProjectDTO>(DTOs, totalItemsCount, query.pageSize, query.pageNumber);

            return result;
        }

        public ProjectDTO GetProjectById(int Id) {

            Project project = projectRepository.GetProjectByIdWithIncludes(Id);

            if (project is null)
                throw new NotFoundException($"There is no project with id: {Id}");

            var result = mapper.Map<ProjectDTO>(project);

            return result;
        }

        public int CreateProject(CreateProject createProject, int teamId) {

            Project project = mapper.Map<Project>(createProject);

            project.OwnerTeamId = teamId;

            projectRepository.CreateProject(project);

            return project.Id;
        }

        public void UpdateProject(CreateProject updatedProject, int Id) {

            Project project = projectRepository.GetProjectById(Id);

            if (project is null)
                throw new NotFoundException($"There is no project with id: {Id}");

            project = mapper.Map(updatedProject, project);

            projectRepository.UpdateProject(project);
        }

        public void DeleteProjectById(int Id) {

            Project project = projectRepository.GetProjectById(Id);

            if (project is null)
                throw new NotFoundException($"There is no project with id: {Id}");

            projectRepository.DeleteProject(project);
        }
    }
}

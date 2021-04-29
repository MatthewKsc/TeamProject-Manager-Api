﻿using AutoMapper;
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
using TeamProject_Manager_Api.Exceptions;

namespace TeamProject_Manager_Api.Services{
    public interface IProjectService {

        List<ProjectDTO> GetAllProjects(int teamId);
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

        public List<ProjectDTO> GetAllProjects(int teamId) {

            ValidTeam(teamId);

            List<Project> projects = context.Projects
                .Where(p => p.OwnerTeamId == teamId)
                .Include(t => t.OwnerTeam)
                .Include(p=>p.UserProjects)
                    .ThenInclude(up => up.User)
                .ToList();

            if (projects.Count < 1)
                throw new NotFoundException("There is no projects to display");

            var result = mapper.Map<List<ProjectDTO>>(projects);

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

            MapUpdatedProject(project, updatedProject);

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

        private void MapUpdatedProject(Project project, CreateProject dto) {
            project.Description = dto.Description;
            project.Title = dto.Title;
            project.StartOfProject = dto.StartOfProject;
            project.EndOfProject = dto.EndOfProject;
        }
    }
}

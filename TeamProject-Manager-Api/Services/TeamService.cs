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
using TeamProject_Manager_Api.Exceptions;

namespace TeamProject_Manager_Api.Services
{
    public interface ITeamService {

        List<TeamDTO> GetAllTeams(int companyId);
        TeamDTO GetTeamById(int companyId, int Id);
        int CreateTeam(CreateTeam createTeam, int companyId);
        void UpdateTeam(CreateTeam updatedTeam, int Id);
        void DeleteTeamById(int companyId, int Id);
       
    }

    public class TeamService : ITeamService{
        
        private readonly ProjectManagerDbContext context;
        private readonly IMapper mapper;

        public TeamService(ProjectManagerDbContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }

        public List<TeamDTO> GetAllTeams(int companyId) {

            ValidCompany(companyId);

            List<Team> teams = context.Teams
                .Where(t => t.CompanyId == companyId)
                .Include(t => t.TeamMembers)
                .ToList();

            if (teams.Count < 1)
                throw new NotFoundException("There is no teams to display");

            var result = mapper.Map<List<TeamDTO>>(teams);

            return result;
        }

        public TeamDTO GetTeamById(int companyId, int Id) {

            ValidCompany(companyId);

            Team team = context.Teams
                .Include(t => t.TeamMembers)
                .SingleOrDefault(t => t.CompanyId == companyId && t.Id == Id);

            if (team is null)
                throw new NotFoundException($"There is no team with id: {Id}");

            var result = mapper.Map<TeamDTO>(team);

            return result;
        }

        public int CreateTeam(CreateTeam createTeam, int companyId) {

            ValidCompany(companyId);

            Team team = mapper.Map<Team>(createTeam);

            team.CompanyId = companyId;

            context.Teams.Add(team);
            context.SaveChanges();

            return team.Id;
        }

        public void UpdateTeam(CreateTeam updatedTeam, int Id) {
            Team team = context.Teams
                .SingleOrDefault(t => t.Id == Id);

            if (team is null)
                throw new NotFoundException($"There is no team with id: {Id}");

            team = mapper.Map(updatedTeam, team);

            context.Update(team);
            context.SaveChanges();
        }

        public void DeleteTeamById(int companyId, int Id) {

            ValidCompany(companyId);

            Team team = context.Teams
                .SingleOrDefault(t => t.CompanyId == companyId && t.Id == Id);

            if (team is null)
                throw new NotFoundException($"There is no team with id: {Id}");

            context.Teams.Remove(team);
            context.SaveChanges();
        }

        private void ValidCompany(int companyId) {
            if (!context.Companies.Any(c => c.Id == companyId))
                throw new BadRequestException($"There is no company with id {companyId}");
        }
    }
}

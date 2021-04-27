using Microsoft.EntityFrameworkCore;
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
    public interface ITeamService {

        List<Team> GetAllTeams(int companyId);

        Team GetTeamById(int companyId, int Id);

        void CreateTeam(Team team, int companyId);

        void DeleteTeamById(int companyId, int Id);
    }

    public class TeamService : ITeamService{
        
        private readonly ProjectManagerDbContext context;

        public TeamService(ProjectManagerDbContext context) {
            this.context = context;
        }

        public List<Team> GetAllTeams(int companyId) {

            ValidCompany(companyId);

            List<Team> teams = context.Teams
                .Where(t => t.CompanyId == companyId)
                .Include(t => t.TeamMembers)
                .Include(t => t.Projects)
                .ToList();

            if (teams.Count < 1)
                throw new NotFoundException("There is no teams to display");

            return teams;
        }

        public Team GetTeamById(int companyId, int Id) {

            ValidCompany(companyId);

            Team team = context.Teams
                .SingleOrDefault(t => t.CompanyId == companyId && t.Id == Id);

            if (team is null)
                throw new NotFoundException($"There is no team with id: {Id}");

            return team;
        }

        public void CreateTeam(Team team, int companyId) {

            ValidCompany(companyId);

            team.CompanyId = companyId;

            context.Teams.Add(team);
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

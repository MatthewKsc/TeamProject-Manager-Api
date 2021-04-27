using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;

namespace TeamProject_Manager_Api.Services
{
    public interface ITeamService {

        List<Team> GetAllTeams(int companyId);

        Team GetTeamById(int companyId, int Id);

        void CreateTeam(Team team, int companyId);

        bool DeleteTeamById(int companyId, int Id);
    }

    public class TeamService : ITeamService{
        
        private readonly ProjectManagerDbContext context;

        public TeamService(ProjectManagerDbContext context) {
            this.context = context;
        }

        public List<Team> GetAllTeams(int companyId) {
            List<Team> teams = context.Teams
                .Where(t => t.CompanyId == companyId)
                .Include(t => t.TeamMembers)
                .Include(t => t.Projects)
                .ToList();

            return teams;
        }

        public Team GetTeamById(int companyId, int Id) {
            Team team = context.Teams
                .SingleOrDefault(t => t.CompanyId == companyId && t.Id == Id);

            return team;
        }

        public void CreateTeam(Team team, int companyId) {
            team.CompanyId = companyId;

            context.Teams.Add(team);
            context.SaveChanges();
        }

        public bool DeleteTeamById(int companyId, int Id) {
            Team team = context.Teams
                .SingleOrDefault(t => t.CompanyId == companyId && t.Id == Id);

            if (team is null)
                return false;

            context.Teams.Remove(team);
            context.SaveChanges();

            return true;
        }
    }
}

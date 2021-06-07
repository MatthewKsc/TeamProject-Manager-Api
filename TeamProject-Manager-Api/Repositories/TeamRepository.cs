using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;

namespace TeamProject_Manager_Api.Repositories
{
    public interface ITeamRepository {
        List<Team> GetTeams(int companyId);
        Team GetTeamById(int Id);
        Team GetTeamByIdWithTeamMembers(int Id);
        void CreateTeam(Team team);
        void UpdateTeam(Team team);
        void DeleteTeam(Team team);
        bool ValidTeam(int Id);
        string GetCompanyDomain(int Id);

    }

    public class TeamRepository : ITeamRepository {

        private readonly ProjectManagerDbContext context;

        public TeamRepository(ProjectManagerDbContext context) {
            this.context = context;
        }

        public List<Team> GetTeams(int companyId) {
            return context.Teams
                .Where(t => t.CompanyId == companyId)
                .Include(t => t.TeamMembers)
                .ToList();
        }

        public Team GetTeamById(int Id) {
            return context.Teams
                .SingleOrDefault(t => t.Id == Id);
        }

        public Team GetTeamByIdWithTeamMembers( int Id) {
            return context.Teams
                .Include(t => t.TeamMembers)
                .SingleOrDefault(t => t.Id == Id);
        }

        public void CreateTeam(Team team) {
            context.Teams.Add(team);
            context.SaveChanges();
        }

        public void UpdateTeam(Team team) {
            context.Update(team);
            context.SaveChanges();
        }

        public void DeleteTeam(Team team) {
            context.Teams.Remove(team);
            context.SaveChanges();
        }

        public bool ValidTeam(int Id) {
            return context.Teams.Any(t => t.Id == Id);
        }

        public string GetCompanyDomain(int Id) {
            return context.Teams
                .Include(t => t.Company)
                .SingleOrDefault(t => t.Id == Id)
                .Company
                .CompanyName
                .ToLower();
        }
    }
}

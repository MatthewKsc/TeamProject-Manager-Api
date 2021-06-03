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
using TeamProject_Manager_Api.Repositories;

namespace TeamProject_Manager_Api.Services
{
    public interface ITeamService {

        List<TeamDTO> GetAllTeams(int companyId);
        TeamDTO GetTeamById(int Id);
        int CreateTeam(CreateTeam createTeam, int companyId);
        void UpdateTeam(CreateTeam updatedTeam, int Id);
        void DeleteTeamById(int Id);
        bool ValidTeam(int Id);
    }

    public class TeamService : ITeamService{

        private readonly ITeamRepository teamRepository;
        private readonly IMapper mapper;

        public TeamService(ITeamRepository teamRepository, IMapper mapper) {
            this.teamRepository = teamRepository;
            this.mapper = mapper;
        }

        public List<TeamDTO> GetAllTeams(int companyId) {

            List<Team> teams = teamRepository.GetTeams(companyId);

            if (teams.Count < 1)
                throw new NotFoundException("There is no teams to display");

            var result = mapper.Map<List<TeamDTO>>(teams);

            return result;
        }

        public TeamDTO GetTeamById(int Id) {

            Team team = teamRepository.GetTeamByIdWithTeamMembers(Id); 

            if (team is null)
                throw new NotFoundException($"There is no team with id: {Id}");

            var result = mapper.Map<TeamDTO>(team);

            return result;
        }

        public int CreateTeam(CreateTeam createTeam, int companyId) {

            Team team = mapper.Map<Team>(createTeam);

            team.CompanyId = companyId;

            teamRepository.CreateTeam(team);

            return team.Id;
        }

        public void UpdateTeam(CreateTeam updatedTeam, int Id) {
            Team team = teamRepository.GetTeamById(Id);

            if (team is null)
                throw new NotFoundException($"There is no team with id: {Id}");

            team = mapper.Map(updatedTeam, team);

            teamRepository.UpdateTeam(team);
        }

        public void DeleteTeamById(int Id) {

            Team team = teamRepository.GetTeamById(Id);

            if (team is null)
                throw new NotFoundException($"There is no team with id: {Id}");

            teamRepository.DeleteTeamById(team);
        }

        public bool ValidTeam(int Id) {
            return teamRepository.ValidTeam(Id);
        }
    }
}

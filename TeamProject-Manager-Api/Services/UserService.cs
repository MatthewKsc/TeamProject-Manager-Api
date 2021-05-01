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
    public interface IUserService {

        List<UserDTO> GetAllUsers(int teamId);
        UserDTO GetUserById(int teamId, int Id);
        int CreateUser(CreateUser createUser, int teamId);
        void UpdateUser(CreateUser updatedUser, int Id);
        void DeleteUserById(int teamId, int Id);
    }

    public class UserService : IUserService{

        private readonly ProjectManagerDbContext context;
        private readonly IMapper mapper;

        public UserService(ProjectManagerDbContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }

        public List<UserDTO> GetAllUsers(int teamId) {

            ValidTeam(teamId);

            List<User> users = context.Users
                .Where(u => u.TeamId == teamId)
                .Include(u => u.Address)
                .Include(u => u.Team)
                .ToList();

            if (users.Count < 1)
                throw new NotFoundException("There is no users to display");

            var result = mapper.Map<List<UserDTO>>(users);

            return result;
        }

        public UserDTO GetUserById(int teamId, int Id) {

            ValidTeam(teamId);

            User user = context.Users
                .Include(u => u.Address)
                .Include(u => u.Team)
                .SingleOrDefault(u => u.TeamId == teamId && u.Id == Id);

            if (user is null)
                throw new NotFoundException($"There is no user with id: {Id}");

            var result = mapper.Map<UserDTO>(user);

            return result;
        }

        public int CreateUser(CreateUser createUser, int teamId) {

            ValidTeam(teamId);

            User user = mapper.Map<User>(createUser);

            string companyDomainEmail = context.Teams
                .Include(t => t.Company)
                .SingleOrDefault(t => t.Id == teamId)
                .Company
                .CompanyName
                .ToLower();

            user.TeamId = teamId;
            user.Email = $"{createUser.FirstName}.{createUser.LastName}@{companyDomainEmail}.com";

            context.Users.Add(user);
            context.SaveChanges();

            return user.Id;
        }

        public void UpdateUser(CreateUser updatedUser, int Id) {
            User user = context.Users
                .Include(u => u.Address)
                .Include(u => u.Team)
                    .ThenInclude(t => t.Company)
                .SingleOrDefault(u=> u.Id == Id);

            if (user is null)
                throw new NotFoundException($"There is no user with id: {Id}");

            string companyDomainEmail = user.Team.Company.CompanyName;

            user = mapper.Map(updatedUser, user);
            user.Email = $"{user.FirstName}.{user.LastName}@{companyDomainEmail}.com";

            context.Update(user);
            context.SaveChanges();
        }

        public void DeleteUserById(int teamId, int Id) {

            ValidTeam(teamId);

            User user = context.Users
                .SingleOrDefault(u => u.TeamId == teamId && u.Id == Id);

            if (user is null)
                throw new NotFoundException($"There is no user with id: {Id}");

            context.Users.Remove(user);
            context.SaveChanges();
        }

        private void ValidTeam(int teamId) {
            if (!context.Teams.Any(t => t.Id == teamId))
                throw new BadRequestException($"There is no team with id {teamId}");
        }
    }
}

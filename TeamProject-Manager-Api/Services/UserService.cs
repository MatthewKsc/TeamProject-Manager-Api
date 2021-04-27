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
    public interface IUserService {

        List<User> GetAllUsers(int teamId);

        User GetUserById(int teamId, int Id);

        void CreateUser(User user, int teamId);

        void DeleteUserById(int teamId, int Id);

    }

    public class UserService : IUserService{

        private readonly ProjectManagerDbContext context;

        public UserService(ProjectManagerDbContext context) {
            this.context = context;
        }

        public List<User> GetAllUsers(int teamId) {

            ValidTeam(teamId);

            List<User> users = context.Users
                .Where(u => u.TeamId == teamId)
                .Include(u => u.Address)
                .ToList();

            if (users.Count < 1)
                throw new NotFoundException("There is no users to display");

            return users;
        }

        public User GetUserById(int teamId, int Id) {

            ValidTeam(teamId);

            User user = context.Users
                .SingleOrDefault(u => u.TeamId == teamId && u.Id == Id);

            if (user is null)
                throw new NotFoundException($"There is no user with id: {Id}");

            return user;
        }

        public void CreateUser(User user, int teamId) {

            ValidTeam(teamId);

            user.TeamId = teamId;

            context.Users.Add(user);
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

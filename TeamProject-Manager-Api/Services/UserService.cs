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
    public interface IUserService {

        List<User> GetAllUsers(int teamId);

        User GetUserById(int teamId, int Id);

        void CreateUser(User user, int teamId);

        bool DeleteUserById(int teamId, int Id);

    }

    public class UserService : IUserService{

        private readonly ProjectManagerDbContext context;

        public UserService(ProjectManagerDbContext context) {
            this.context = context;
        }

        public List<User> GetAllUsers(int teamId) {
            List<User> users = context.Users
                .Where(u => u.TeamId == teamId)
                .Include(u => u.Address)
                .ToList();

            return users;
        }

        public User GetUserById(int teamId, int Id) {
            User user = context.Users
                .SingleOrDefault(u => u.TeamId == teamId && u.Id == Id);

            return user;
        }

        public void CreateUser(User user, int teamId) {
            user.TeamId = teamId;

            context.Users.Add(user);
            context.SaveChanges();
        }

        public bool DeleteUserById(int teamId, int Id) {
            User user = context.Users
                .SingleOrDefault(u => u.TeamId == teamId && u.Id == Id);

            if (user is null)
                return false;

            context.Users.Remove(user);
            context.SaveChanges();

            return true;
        }
    }
}

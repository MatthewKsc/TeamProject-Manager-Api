using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Dtos.Querying_Models;

namespace TeamProject_Manager_Api.Repositories
{
    public interface IUserRepository {
        IQueryable<User> GetUsersQuery(Query<UserDTO> query, int teamId);
        List<User> GetUsersWithQuery(Query<UserDTO> query, IQueryable<User> baseQuery);
        List<User> GetUsers(int teamId);
        User GetUserById(int Id);
        User GetUserByIdWithIncludes(int Id);
        void CreateUser(User user);
        void UpdateUser(User updatedUser);
        void DeleteUser(User user);
        User GetUserByEmail(string userEmail);
        List<User> GetUserByEmail(List<string> userEmail);
    }

    public class UserRepository : IUserRepository {

        private readonly ProjectManagerDbContext context;

        public UserRepository(ProjectManagerDbContext context) {
            this.context = context;
        }

        public User GetUserById(int Id) {
            return context.Users
                .SingleOrDefault(u => u.Id == Id);
        }

        public User GetUserByIdWithIncludes(int Id) {
            return context.Users
                .Include(u => u.Address)
                .Include(u => u.Team)
                    .ThenInclude(t => t.Company)
                .SingleOrDefault(u => u.Id == Id);
        }

        public List<User> GetUsers(int teamId) {
            return context.Users
                .Where(u => u.TeamId == teamId)
                .Include(u => u.Address)
                .Include(u => u.Team)
                .ToList();
        }

        public IQueryable<User> GetUsersQuery(Query<UserDTO> query, int teamId) {
            return context.Users
                .Where(u => u.TeamId == teamId)
                .Include(u => u.Address)
                .Include(u => u.Team)
                .Where(u => string.IsNullOrEmpty(query.searchPhrase) || string.IsNullOrWhiteSpace(query.searchPhrase) ||
                    (
                        u.FirstName.ToLower().Contains(query.searchPhrase.ToLower()) ||
                        u.LastName.ToLower().Contains(query.searchPhrase.ToLower()) ||
                        u.Email.ToLower().Contains(query.searchPhrase.ToLower())
                    )
                );
        }

        public List<User> GetUsersWithQuery(Query<UserDTO> query, IQueryable<User> baseQuery) {
            return baseQuery
                .Skip(query.pageSize * (query.pageNumber - 1))
                .Take(query.pageSize)
                .ToList();
        }

        public void CreateUser(User user) {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void UpdateUser(User updatedUser) {
            context.Update(updatedUser);
            context.SaveChanges();
        }

        public void DeleteUser(User user) {
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public User GetUserByEmail(string userEmail) {
            return context.Users.SingleOrDefault(u => u.Email.ToLower().Equals(userEmail.ToLower()));
        }

        public List<User> GetUserByEmail(List<string> userEmail) {
            return context.Users
                .Where(u => userEmail.Contains(u.Email)).ToList();
        }
    }
}

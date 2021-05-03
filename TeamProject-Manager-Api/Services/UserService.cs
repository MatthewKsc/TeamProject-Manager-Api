using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Dtos.Models_Operations;
using TeamProject_Manager_Api.Dtos.Querying_Models;
using TeamProject_Manager_Api.Exceptions;

namespace TeamProject_Manager_Api.Services
{
    public interface IUserService {

        PageResult<UserDTO> GetAllUsers(Query<UserDTO> query, int teamId);
        UserDTO GetUserById(int teamId, int Id);
        int CreateUser(CreateUser createUser, int teamId);
        void UpdateUser(CreateUser updatedUser, int Id);
        void DeleteUserById(int teamId, int Id);
    }

    public class UserService : IUserService{

        private readonly ProjectManagerDbContext context;
        private readonly IMapper mapper;
        private readonly char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public UserService(ProjectManagerDbContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }

        public PageResult<UserDTO> GetAllUsers(Query<UserDTO> query, int teamId) {

            ValidTeam(teamId);

            IQueryable<User> baseResult = context.Users
                .Where(u => u.TeamId == teamId)
                .Include(u => u.Address)
                .Include(u => u.Team)
                .Where(u => query.searchPhrase == null ||
                    (
                        u.FirstName.ToLower().Contains(query.searchPhrase.ToLower()) ||
                        u.LastName.ToLower().Contains(query.searchPhrase.ToLower()) ||
                        u.Email.ToLower().Contains(query.searchPhrase.ToLower())
                    )
                );

            if (!string.IsNullOrEmpty(query.SortBy)) {

                var columnsToSortBy = new Dictionary<string, Expression<Func<User, object>>>() {
                    {nameof(User.FirstName), u=> u.FirstName},
                    {nameof(User.LastName), u=> u.LastName},
                    {nameof(User.Team), u=> u.Team}
                };

                var selectedColumn = columnsToSortBy[query.SortBy];

                baseResult = query.SortDirection == SortDirection.ASC ? 
                    baseResult.OrderBy(selectedColumn) : baseResult.OrderByDescending(selectedColumn);
            }

            List<User> users = baseResult
                .Skip(query.pageSize * (query.pageNumber - 1))
                .Take(query.pageSize)
                .ToList();

            int totalItemCount = baseResult.Count();

            var DTOs = mapper.Map<List<UserDTO>>(users);

            var result = new PageResult<UserDTO>(DTOs, totalItemCount, query.pageSize, query.pageNumber);

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
            user.LastName = user.LastName.TrimEnd(digits);

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
            user.LastName = user.LastName.TrimEnd(digits);

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

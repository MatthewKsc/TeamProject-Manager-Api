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
using TeamProject_Manager_Api.Repositories;

namespace TeamProject_Manager_Api.Services
{
    public interface IUserService {

        PageResult<UserDTO> GetAllUsers(Query<UserDTO> query, int teamId);
        UserDTO GetUserById(int Id);
        int CreateUser(CreateUser createUser, int teamId);
        void UpdateUser(CreateUser updatedUser, int Id);
        void DeleteUserById(int Id);
    }

    public class UserService : IUserService{

        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ITeamService teamService;
        private readonly char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public UserService(IUserRepository userRepository, IMapper mapper, ITeamService teamService) {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.teamService = teamService;
        }

        public PageResult<UserDTO> GetAllUsers(Query<UserDTO> query, int teamId) {

            IQueryable<User> baseResult = userRepository.GetUsersQuery(query, teamId);

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

            List<User> users = userRepository.GetUsersWithQuery(query, baseResult);

            int totalItemCount = users.Count();

            var DTOs = mapper.Map<List<UserDTO>>(users);

            var result = new PageResult<UserDTO>(DTOs, totalItemCount, query.pageSize, query.pageNumber);

            return result;
        }

        public UserDTO GetUserById(int Id) {

            User user = userRepository.GetUserByIdWithIncludes(Id);

            if (user is null)
                throw new NotFoundException($"There is no user with id: {Id}");

            var result = mapper.Map<UserDTO>(user);

            return result;
        }

        public int CreateUser(CreateUser createUser, int teamId) {

            User user = mapper.Map<User>(createUser);

            string companyDomainEmail = teamService.GetCompanyDomain(teamId);

            user.TeamId = teamId;
            user.Email = $"{createUser.FirstName}.{createUser.LastName}@{companyDomainEmail}.com";
            user.LastName = user.LastName.TrimEnd(digits);

            userRepository.CreateUser(user);

            return user.Id;
        }

        public void UpdateUser(CreateUser updatedUser, int Id) {

            User user = userRepository.GetUserByIdWithIncludes(Id);

            if (user is null)
                throw new NotFoundException($"There is no user with id: {Id}");

            string companyDomainEmail = user.Team.Company.CompanyName;

            user = mapper.Map(updatedUser, user);
            user.Email = $"{user.FirstName}.{user.LastName}@{companyDomainEmail}.com";
            user.LastName = user.LastName.TrimEnd(digits);

            userRepository.UpdateUser(user);
        }

        public void DeleteUserById( int Id) {

            User user = userRepository.GetUserById(Id);

            if (user is null)
                throw new NotFoundException($"There is no user with id: {Id}");

            userRepository.DeleteUser(user);
        }

    }
}

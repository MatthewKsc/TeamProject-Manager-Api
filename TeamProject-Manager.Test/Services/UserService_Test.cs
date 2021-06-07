using AutoMapper;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TeamProject_Manager_Api;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Dtos.Models_Operations;
using TeamProject_Manager_Api.Dtos.Querying_Models;
using TeamProject_Manager_Api.Exceptions;
using TeamProject_Manager_Api.Repositories;
using TeamProject_Manager_Api.Services;

namespace TeamProject_Manager.Test.Services
{
    [TestFixture]
    public class UserService_Test{

        private static readonly MappingProfile profile = new MappingProfile();
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile(profile));

        private readonly Mock<ITeamService> teamServiceMock = new Mock<ITeamService>();

        private readonly UserService userService;
        private readonly Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();

        public UserService_Test() {
            IMapper mapper = new Mapper(config);
            userService = new UserService(userRepoMock.Object, mapper , teamServiceMock.Object);
        }

        [Test]
        public void GetAllUsers_Test() {
            var query = new Query<UserDTO>() {
                pageNumber = 1,
                pageSize = 5
            };
            var teamId = 1;
            var users = GetUsers();
            IQueryable<User> baseQuery = Enumerable.Empty<User>().AsQueryable();
            userRepoMock.Setup(x => x.GetUsersQuery(query, teamId)).Returns(baseQuery);
            userRepoMock.Setup(x => x.GetUsersWithQuery(query, baseQuery)).Returns(users);

            var result = userService.GetAllUsers(query, teamId);

            Assert.AreEqual(users.Count, result.Items.Count);
            Assert.AreEqual(users.Count, result.TotalItemsCount);
            Assert.AreNotEqual(10, result.TotalItemsCount);
        }

        [Test]
        public void GetUserById_Test() {
            var user = GetUser();
            userRepoMock.Setup(x => x.GetUserByIdWithIncludes(user.Id)).Returns(user);

            var result = userService.GetUserById(user.Id);

            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreNotEqual("InvalidString", result.LastName);

            Assert.Throws<NotFoundException>(() => userService.GetUserById(0));
        }

        [Test]
        public void CreateUser_Test() {
            var createUser = new CreateUser(){FirstName ="test", LastName="test"};
            userRepoMock.Setup(x => x.CreateUser(It.IsAny<User>())).Verifiable();
            teamServiceMock.Setup(x => x.GetCompanyDomain(1)).Returns("test");

            userService.CreateUser(createUser, 1);

            userRepoMock.Verify(x => x.CreateUser(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void UpdateProject_Test() {
            var createUser = new CreateUser() { FirstName = "test", LastName = "test" };
            var user = GetUser();
            userRepoMock.Setup(x => x.UpdateUser(It.IsAny<User>())).Verifiable();
            userRepoMock.Setup(x => x.GetUserByIdWithIncludes(user.Id)).Returns(user);

            userService.UpdateUser(createUser, user.Id);

            userRepoMock.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Once);
            Assert.Throws<NotFoundException>(() => userService.UpdateUser(createUser, 0));
        }


        [Test]
        public void DeleteProjectById_Test() {
            var user = GetUser();
            userRepoMock.Setup(x => x.UpdateUser(It.IsAny<User>())).Verifiable();
            userRepoMock.Setup(x => x.GetUserById(user.Id)).Returns(user);

            userService.DeleteUserById(user.Id);

            userRepoMock.Verify(x => x.DeleteUser(It.IsAny<User>()), Times.Once);
            Assert.Throws<NotFoundException>(() => userService.DeleteUserById(0));
        }

        [Ignore("not a test method")]
        private User GetUser() {
            return new User {
                Id = 1,
                FirstName ="FirstName_Test",
                LastName = "LastName_Test",
                Email ="test@test.com",
                Team = new Team { 
                    Id = 1, 
                    Company = new Company{CompanyName ="test" } 
                }
            };
        }

        [Ignore("not a test method")]
        private List<User> GetUsers() {
            Team team = new Team { Id=1, NameOfTeam = "test name" };
            return new List<User>(){
                new User {
                    Id = 1,
                    FirstName ="FirstName_Test",
                    LastName = "LastName_Test",
                    Email ="test@test.com",
                    Team = team
                },
                new User {
                    Id = 2,
                    FirstName ="FirstName_Test",
                    LastName = "LastName_Test",
                    Email ="test@test.com",
                    Team = team
                }
            };
        }
    }
}

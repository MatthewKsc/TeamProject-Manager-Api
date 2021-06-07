using AutoMapper;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TeamProject_Manager_Api;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Exceptions;
using TeamProject_Manager_Api.Repositories;
using TeamProject_Manager_Api.Services;

namespace TeamProject_Manager.Test.Services
{
    [TestFixture]
    public class UserProjectsService_Test{

        private static readonly MappingProfile profile = new MappingProfile();
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile(profile));

        private readonly UserProjectsService userProjectsService;
        private readonly Mock<IUserProjectsRepository> userProjectsRepoMock = new Mock<IUserProjectsRepository>();
        private readonly Mock<IProjectRepository> projectRepoMock = new Mock<IProjectRepository>();
        private readonly Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();

        public UserProjectsService_Test() {
            IMapper mapper = new Mapper(config);
            userProjectsService = new UserProjectsService(userProjectsRepoMock.Object, projectRepoMock.Object, userRepoMock.Object);
        }

        [Test]
        public void AddUserToProject_Test() {
            var user = GetUser();
            var project = GetProject();
            projectRepoMock.Setup(x => x.GetProjectById(project.Id)).Returns(project);
            userRepoMock.Setup(x => x.GetUserByEmail(user.Email)).Returns(user);
            userProjectsRepoMock.Setup(x => x.AddUserProject(It.IsAny<UserProject>())).Verifiable();

            userProjectsService.AddUserToProject(user.Email, project.Id);

            userProjectsRepoMock.Verify(x => x.AddUserProject(It.IsAny<UserProject>()), Times.Once);
            Assert.Throws<NotFoundException>(() => userProjectsService.AddUserToProject(user.Email, 0));
            Assert.Throws<NotFoundException>(() => userProjectsService.AddUserToProject("", project.Id));
            Assert.DoesNotThrow(() => userProjectsService.AddUserToProject(user.Email, project.Id));

        }

        [Test]
        public void AddListOfUsersToProject_Test() {
            var users = GetUsers();
            var userEmails = users.Select(u => u.Email).ToList();
            var project = GetProject();
            projectRepoMock.Setup(x => x.GetProjectById(project.Id)).Returns(project);
            userRepoMock.Setup(x => x.GetUserByEmail(userEmails)).Returns(users);
            userProjectsRepoMock.Setup(x => x.AddUserProject(It.IsAny<List<UserProject>>())).Verifiable();

            userProjectsService.AddUserToProject(userEmails, project.Id);

            userProjectsRepoMock.Verify(x => x.AddUserProject(It.IsAny<List<UserProject>>()), Times.Once);
            Assert.Throws<NotFoundException>(() => userProjectsService.AddUserToProject(userEmails, 0));
            Assert.Throws<NotFoundException>(() => userProjectsService.AddUserToProject("", project.Id));
            Assert.DoesNotThrow(() => userProjectsService.AddUserToProject(userEmails, project.Id));

        }

        [Test]
        public void RemoveUserFromProject_Test() {
            var user = GetUser();
            var project = GetProject();
            var fakeUser = new User { Id = 2, Email = "second test" };
            projectRepoMock.Setup(x => x.GetProjectById(project.Id)).Returns(project);
            userRepoMock.Setup(x => x.GetUserByEmail(user.Email)).Returns(user);
            userRepoMock.Setup(x => x.GetUserByEmail(fakeUser.Email)).Returns(fakeUser);

            userProjectsRepoMock.Setup(x => x.GetUserProject(project.Id, user.Id)).Returns(new UserProject());
            userProjectsRepoMock.Setup(x => x.DeleteUserProject(It.IsAny<UserProject>())).Verifiable();

            userProjectsService.RemoveUserFromProject(user.Email, project.Id);

            userProjectsRepoMock.Verify(x => x.DeleteUserProject(It.IsAny<UserProject>()), Times.Once);
            Assert.Throws<NotFoundException>(() => userProjectsService.RemoveUserFromProject(user.Email, 0));
            Assert.Throws<NotFoundException>(() => userProjectsService.RemoveUserFromProject("", project.Id));
            Assert.Throws<NotFoundException>(() => userProjectsService.RemoveUserFromProject(fakeUser.Email, project.Id));
            Assert.DoesNotThrow(() => userProjectsService.RemoveUserFromProject(user.Email, project.Id));

        }

        [Ignore("not a test method")]
        private User GetUser() {
            return new User {
                Id = 1,
                FirstName = "FirstName_Test",
                LastName = "LastName_Test",
                Email = "test@test.com"
            };
        }

        [Ignore("not a test method")]
        private List<User> GetUsers() {
            return new List<User>(){
                new User {
                    Id = 1,
                    FirstName ="FirstName_Test",
                    LastName = "LastName_Test",
                    Email ="test@test.com"
                },
                new User {
                    Id = 2,
                    FirstName ="FirstName_Test",
                    LastName = "LastName_Test",
                    Email ="test@test.com"
                }
            };
        }

        private Project GetProject() {
            return new Project {
                Id = 1,
                Title = "TestProject",
                Description = "Test description"
            };
        }
    }
}

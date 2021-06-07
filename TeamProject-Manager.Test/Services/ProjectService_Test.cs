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
    public class ProjectService_Test{

        private static readonly MappingProfile profile = new MappingProfile();
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile(profile));

        private readonly ProjectService projectService;
        private readonly Mock<IProjectRepository> projectRepoMock = new Mock<IProjectRepository>();

        public ProjectService_Test() {
            IMapper mapper = new Mapper(config);
            projectService = new ProjectService(projectRepoMock.Object, mapper);
        }

        [Test]
        public void GetAllProjects_Test() {
            var query = new Query<ProjectDTO>() {
                pageNumber = 1,
                pageSize = 5
            };
            var teamId = 1;
            var projects = GetProjects();
            IQueryable<Project> baseQuery = Enumerable.Empty<Project>().AsQueryable();
            projectRepoMock.Setup(x => x.GetProjectQuery(query, teamId)).Returns(baseQuery);
            projectRepoMock.Setup(x => x.GetProjectsWithQuery(query, baseQuery)).Returns(projects);

            var result = projectService.GetAllProjects(query, teamId);

            Assert.AreEqual(projects.Count, result.Items.Count);
            Assert.AreEqual(projects.Count, result.TotalItemsCount);
            Assert.AreNotEqual(10, result.TotalItemsCount);
        }

        [Test]
        public void GetProejctById_Test() {
            var project = GetProject();
            projectRepoMock.Setup(x => x.GetProjectByIdWithIncludes(project.Id)).Returns(project);

            var result = projectService.GetProjectById(project.Id);

            Assert.AreEqual(project.Id, result.Id);
            Assert.AreEqual(project.OwnerTeam.NameOfTeam, result.ResponsibleTeam);
            Assert.AreEqual(project.Title, result.Title);
            Assert.AreNotEqual("InvalidString", result.Title);

            Assert.Throws<NotFoundException>(() => projectService.GetProjectById(0));
        }

        [Test]
        public void CreateProject_Test() {
            var createProejct = new CreateProject();
            projectRepoMock.Setup(x => x.CreateProject(It.IsAny<Project>())).Verifiable();

            projectService.CreateProject(createProejct, 1);

            projectRepoMock.Verify(x => x.CreateProject(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public void UpdateProject_Test() {
            var createProejct = new CreateProject();
            var project = GetProject();
            projectRepoMock.Setup(x => x.UpdateProject(It.IsAny<Project>())).Verifiable();
            projectRepoMock.Setup(x => x.GetProjectById(project.Id)).Returns(project);

            projectService.UpdateProject(createProejct, project.Id);

            projectRepoMock.Verify(x => x.UpdateProject(It.IsAny<Project>()), Times.Once);
            Assert.Throws<NotFoundException>(() => projectService.UpdateProject(createProejct, 0));
        }


        [Test]
        public void DeleteProject() {
            var project = GetProject();
            projectRepoMock.Setup(x => x.DeleteProject(It.IsAny<Project>())).Verifiable();
            projectRepoMock.Setup(x => x.GetProjectById(project.Id)).Returns(project);

            projectService.DeleteProject(project.Id);

            projectRepoMock.Verify(x => x.DeleteProject(It.IsAny<Project>()), Times.Once);
            Assert.Throws<NotFoundException>(() => projectService.DeleteProject(0));
        }

        [Ignore("not a test method")]
        private Project GetProject() {
            return new Project {
                Id = 1,
                Title = "TestProject",
                Description = "Test description",
                OwnerTeam = new Team { NameOfTeam = "test name" },
                UserProjects = new List<UserProject>()
            };
        }

        [Ignore("not a test method")]
        private List<Project> GetProjects() {
            return new List<Project>(){
                new Project { Id = 1,  
                    Title = "TestProject", 
                    Description = "Test description", 
                    OwnerTeam = new Team { NameOfTeam = "test name" },
                    UserProjects = new List<UserProject>() },

                new Project { Id = 2,
                    Title = "TestProject2",
                    Description = "Test description2",
                    OwnerTeam = new Team { NameOfTeam = "test name" },
                    UserProjects = new List<UserProject>() }
            };
        }

    }
}

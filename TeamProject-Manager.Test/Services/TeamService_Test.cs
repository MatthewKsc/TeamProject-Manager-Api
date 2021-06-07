using AutoMapper;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TeamProject_Manager_Api;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Dtos.Models_Operations;
using TeamProject_Manager_Api.Exceptions;
using TeamProject_Manager_Api.Repositories;
using TeamProject_Manager_Api.Services;

namespace TeamProject_Manager.Test.Services
{
    [TestFixture]
    public class TeamService_Test{

        private static readonly MappingProfile profile = new MappingProfile();
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile(profile));

        private readonly TeamService teamSearvice;
        private readonly Mock<ITeamRepository> teamRepoMock = new Mock<ITeamRepository>();

        public TeamService_Test() {
            IMapper mapper = new Mapper(config);
            teamSearvice = new TeamService(teamRepoMock.Object, mapper);
        }

        [Test]
        public void GetAllTeams_Test() {
            Company company = new Company() { Id = 1 };
            Company exception = new Company() { Id = 2 };
            var teams = GetTeams(company);
            teamRepoMock.Setup(x => x.GetTeams(company.Id)).Returns(teams);
            teamRepoMock.Setup(x => x.GetTeams(exception.Id)).Returns(new List<Team>());

            var result = teamSearvice.GetAllTeams(company.Id);

            Assert.AreEqual(teams.Count, result.Count);
            Assert.AreNotEqual(10, result.Count);
            Assert.Throws<NotFoundException>(() => teamSearvice.GetAllTeams(exception.Id));
        }

        [Test]
        public void GetProejctById_Test() {
            var team = GetTeam();
            teamRepoMock.Setup(x => x.GetTeamByIdWithTeamMembers(team.Id)).Returns(team);

            var result = teamSearvice.GetTeamById(team.Id);

            Assert.AreEqual(team.Id, result.Id);
            Assert.AreEqual(team.NameOfTeam, result.NameOfTeam);
            Assert.AreEqual(team.TeamMembers.Count, result.Users.Count);
            Assert.AreNotEqual("InvalidString", result.NameOfTeam);

            Assert.Throws<NotFoundException>(() => teamSearvice.GetTeamById(0));
        }

        [Test]
        public void CreateProject_Test() {
            var createTeam = new CreateTeam();
            teamRepoMock.Setup(x => x.CreateTeam(It.IsAny<Team>())).Verifiable();

            teamSearvice.CreateTeam(createTeam, 1);

            teamRepoMock.Verify(x => x.CreateTeam(It.IsAny<Team>()), Times.Once);
        }

        [Test]
        public void UpdateProject_Test() {
            var createTeam = new CreateTeam();
            var team = GetTeam();
            teamRepoMock.Setup(x => x.UpdateTeam(It.IsAny<Team>())).Verifiable();
            teamRepoMock.Setup(x => x.GetTeamById(team.Id)).Returns(team);

            teamSearvice.UpdateTeam(createTeam, team.Id);

            teamRepoMock.Verify(x => x.UpdateTeam(It.IsAny<Team>()), Times.Once);
            Assert.Throws<NotFoundException>(() => teamSearvice.UpdateTeam(createTeam, 0));
        }


        [Test]
        public void DeleteProjectById_Test() {
            var team = GetTeam();
            teamRepoMock.Setup(x => x.DeleteTeam(It.IsAny<Team>())).Verifiable();
            teamRepoMock.Setup(x => x.GetTeamById(team.Id)).Returns(team);

            teamSearvice.DeleteTeamById(team.Id);

            teamRepoMock.Verify(x => x.DeleteTeam(It.IsAny<Team>()), Times.Once);
            Assert.Throws<NotFoundException>(() => teamSearvice.DeleteTeamById(0));
        }

        [Test]
        public void ValidTeam_Test() {
            teamRepoMock.Setup(x => x.ValidTeam(1)).Returns(true);

            teamSearvice.ValidTeam(1);

            teamRepoMock.Verify(x => x.ValidTeam(1), Times.Once);
            Assert.Throws<BadRequestException>(() => teamSearvice.ValidTeam(2));
            Assert.DoesNotThrow(() => teamSearvice.ValidTeam(1));
        }

        [Ignore("not a test method")]
        private Team GetTeam() {
            return new Team {
                Id = 1,
                NameOfTeam = "Test Team",
                Company = new Company{Id =1},
                TeamMembers = new List<User>()
            };
        }

        [Ignore("not a test method")]
        private List<Team> GetTeams(Company company) {
            return new List<Team>(){
                new Team {
                Id = 1,
                NameOfTeam = "Test Team",
                Company = company
                },

                new Team {
                    Id = 1,
                    NameOfTeam = "Test Team",
                    Company = company
                }
            };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;


//to fill up database with data
namespace TeamProject_Manager_Api {
    public class ProjectManagerSeeder {

        private readonly ProjectManagerDbContext context;

        public ProjectManagerSeeder(ProjectManagerDbContext context) {
            this.context = context;
        }

        public void Seed() {
            if (context.Database.CanConnect()) {
                if (!context.Companies.Any()) {
                    var companies = CreatComapny();

                    context.Companies.AddRange(companies);
                    context.SaveChanges();
                }
            }
        }

        private IEnumerable<Company> CreatComapny() {

            var comapnies = new List<Company>() {
                new Company() {
                    CompanyName ="SentorioSTR",
                    SizeOfComapny = 1500,
                    Address = DefaultAddress(),
                    Teams = CreateTeams()
                }
            };

            return comapnies;
        }

        private ICollection<Team> CreateTeams() {
            var projectNet = DefaulProcjet();
            var projectOracle = DefaulProcjet();

            var usersNET = new List<User>() {
                CreatUser("Kaufo", "Cheinz"),
                CreatUser("Serafine", "Som"),
            };

            var usersOracle = new List<User>() {
                CreatUser("Matt", "Shein"),
                CreatUser("Serain", "Alento")
            };

            var teams = new List<Team>() {
                new Team(){
                    NameOfTeam="Oracle dev",
                    TeamMembers= usersOracle,
                    Projects= projectOracle
                },
                new Team(){
                    NameOfTeam=".NET dev",
                    TeamMembers= usersNET,
                    Projects = projectNet
                },
            };

            var userProjectNet = UserProject.AddManyUsersToProject(usersNET, projectNet[0]);
            var userProjectsOracle = UserProject.AddManyUsersToProject(usersOracle, projectOracle[0]);
            context.AddRange(userProjectNet);
            context.AddRange(userProjectsOracle);

            return teams;
        }

        private User CreatUser(string firstName, string lastName) {
            return new User() {
                FirstName = firstName,
                LastName = lastName,
                Email = $"{firstName}.{lastName}@str.com",
                Address = DefaultAddress(),
                DateOfBirth = DateTime.Parse("1990-01-01"),
            };
        }

        //only need to fill up basic data in api
        private Address DefaultAddress() {
            return new Address() { Country = "Poland", City = "Katowice", Street = "Katowicka", PostalCode = "41-723" };
        }

        private List<Project> DefaulProcjet() {
            return new List<Project>() {
                new Project() {
                    Title = "Maintain of applications",
                    Description = "Default job to maintain application from users that already are deployed"
                }
            };
        }
    } 
}

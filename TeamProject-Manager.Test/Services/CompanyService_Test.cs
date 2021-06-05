using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TeamProject_Manager_Api;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Repositories;
using TeamProject_Manager_Api.Services;

namespace TeamProject_Manager.Test.Services
{
    [TestFixture]
    public class CompanyService_Test
    {
        private static readonly MappingProfile profile = new MappingProfile();
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile(profile));

        private readonly CompanyService comapnyService;
        private readonly Mock<ICompanyRepository> companyRepoMock = new Mock<ICompanyRepository>();

        public CompanyService_Test() {
            IMapper mapper = new Mapper(config);
            comapnyService = new CompanyService(companyRepoMock.Object, mapper);
        }

        [Test]
        public void GetComapnyById_Test(){

            //Arrange
            var company = getComapny();
            companyRepoMock.Setup(x => x.GetComapnyByIdWithAddress(1)).Returns(company);

            //Act
            var result = comapnyService.GetComapnyById(company.Id);

            //Assert
            Assert.AreEqual(company.Id, result.Id);
            Assert.AreEqual(company.CompanyName, result.CompanyName);

        }

        [Ignore("not a test method")]
        private Company getComapny() {
            return new Company {
                Id = 1,
                CompanyName = "TestCompany",
                SizeOfComapny = 100,
                Address = new Address(),
                Teams = new List<Team>()
            };
        }
    }
}

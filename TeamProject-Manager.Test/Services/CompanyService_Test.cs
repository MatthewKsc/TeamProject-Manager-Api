using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TeamProject_Manager_Api;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Dtos.Models_Operations;
using TeamProject_Manager_Api.Exceptions;
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

            Assert.Throws<NotFoundException>(() => comapnyService.GetComapnyById(0));

        }

        [Test]
        public void GetAllComapnies_Test() {
            var comapnies = getComapnies();
            companyRepoMock.Setup(x => x.GetAllComapnies()).Returns(comapnies);

            var result = comapnyService.GetAllComapnies();

            Assert.IsNotNull(result);
            Assert.AreEqual(comapnies.Count, result.Count);
        }

        [Test]
        public void GetAllComapnies_EmptyListThrowException_Test() {
            companyRepoMock.Setup(x => x.GetAllComapnies()).Returns(new List<Company>());

            Assert.Throws<NotFoundException>(() => comapnyService.GetAllComapnies());
        }

        [Test]
        public void CreateCompany_Test(){
            var createComapny = new CreateCompany();
            companyRepoMock.Setup(x => x.CreateCompany(It.IsAny<Company>())).Verifiable();

            comapnyService.CreateCompany(createComapny);

            companyRepoMock.Verify(x => x.CreateCompany(It.IsAny<Company>()), Times.Once);
        }

        [Test]
        public void UpdateCompany_Test() {
            var createComapny = new CreateCompany();
            var company = getComapny();
            companyRepoMock.Setup(x => x.UpdateComapny(It.IsAny<Company>())).Verifiable();
            companyRepoMock.Setup(x => x.GetComapnyByIdWithAddress(company.Id))
                .Returns(company);

            comapnyService.UpdateComapny(createComapny, company.Id);

            companyRepoMock.Verify(x => x.UpdateComapny(It.IsAny<Company>()), Times.Once);
            Assert.Throws<NotFoundException>(() => comapnyService.UpdateComapny(createComapny, 2));
        }

        [Test]
        public void DeleteComapnyById_Test() {
            var company = getComapny();
            companyRepoMock.Setup(x => x.DeleteComapny(It.IsAny<Company>())).Verifiable();
            companyRepoMock.Setup(x => x.GetComapnyById(company.Id))
                .Returns(company);

            comapnyService.DeleteComapnyById(company.Id);

            companyRepoMock.Verify(x => x.DeleteComapny(It.IsAny<Company>()), Times.Once);
            Assert.Throws<NotFoundException>(() => comapnyService.DeleteComapnyById(2));
        }

        [Test]
        public void ValidCompany_Test() {
            companyRepoMock.Setup(x => x.ValidComapny(1)).Returns(true);

            comapnyService.ValidCompany(1);

            companyRepoMock.Verify(x => x.ValidComapny(1), Times.Once);
            Assert.Throws<BadRequestException>(() => comapnyService.ValidCompany(2));
        }

        [Ignore("not a test method")]
        private Company getComapny() {
            return new Company {
                Id = 1,
                CompanyName = "TestCompany",
                SizeOfComapny = 100,
                Address = new Address()
            };
        }

        [Ignore("not a test method")]
        private List<Company> getComapnies() {
            return new List<Company>(){
                new Company { Id = 1, CompanyName = "TestCompany", SizeOfComapny = 100, Address = new Address() },
                new Company { Id = 2, CompanyName = "TestCompany", SizeOfComapny = 100, Address = new Address() }
            };
        }
    }
}

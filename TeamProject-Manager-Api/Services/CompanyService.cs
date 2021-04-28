using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Exceptions;
using AutoMapper;
using TeamProject_Manager_Api.Dtos.Models;

namespace TeamProject_Manager_Api.Services
{
    public interface ICompanyService {

        List<Company> GetAllComapnies();
        CompanyDTO GetComapnyById(int Id);
        void CreateCompany(Company company);
        void DeleteComapnyById(int Id);
    }

    public class CompanyService : ICompanyService{

        private readonly ProjectManagerDbContext context;
        private readonly IMapper mapper;

        public CompanyService(ProjectManagerDbContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }

        public List<Company> GetAllComapnies() {

            List<Company> companies= context.Companies
                .Include(c => c.Address)
                .Include(c => c.Teams)
                    .ThenInclude(t => t.Projects)
                .Include(c => c.Teams)
                     .ThenInclude(t => t.TeamMembers)
                     .ToList();

            if (companies.Count < 1)
                throw new NotFoundException("There is no companies to display");

            return companies;
        }

        public CompanyDTO GetComapnyById(int Id) {
            Company company = context.Companies
                .Include(c => c.Address)
                .SingleOrDefault(c => c.Id == Id);

            if (company is null)
                throw new NotFoundException($"There is no company with id: {Id}");

            var result = mapper.Map<CompanyDTO>(company);

            return result;
        }

        public void CreateCompany(Company company) {
            context.Companies.Add(company);
            context.SaveChanges();
        }

        public void DeleteComapnyById(int Id) {
            Company company = context.Companies.SingleOrDefault(c => c.Id == Id);

            if(company is null)
                throw new NotFoundException($"There is no company with id: {Id}");

            context.Companies.Remove(company);
            context.SaveChanges();
        }
    }
}

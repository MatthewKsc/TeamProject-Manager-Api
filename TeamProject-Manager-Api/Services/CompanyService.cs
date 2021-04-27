using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Exceptions;

namespace TeamProject_Manager_Api.Services
{
    public interface ICompanyService {

        List<Company> GetAllComapnies();
        Company GetComapnyById(int Id);
        void CreateCompany(Company company);
        void DeleteComapnyById(int Id);
    }

    public class CompanyService : ICompanyService{

        private readonly ProjectManagerDbContext context;

        public CompanyService(ProjectManagerDbContext context) {
            this.context = context;
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

        public Company GetComapnyById(int Id) {
            Company company = context.Companies
                .SingleOrDefault(c => c.Id == Id);

            if (company is null)
                throw new NotFoundException($"There is no company with id: {Id}");

            return company;
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

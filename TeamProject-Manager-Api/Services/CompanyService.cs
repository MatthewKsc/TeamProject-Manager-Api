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
        bool DeleteComapnyById(int Id);
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

            return companies;
        }

        public Company GetComapnyById(int Id) {
            Company company = context.Companies.SingleOrDefault(c => c.Id == Id);

            return company;
        }

        public void CreateCompany(Company company) {
            context.Companies.Add(company);
            context.SaveChanges();
        }

        public bool DeleteComapnyById(int Id) {
            Company company = context.Companies.SingleOrDefault(c => c.Id == Id);

            if(company is null)
                return false;

            context.Companies.Remove(company);
            context.SaveChanges();

            return true;
        }
    }
}

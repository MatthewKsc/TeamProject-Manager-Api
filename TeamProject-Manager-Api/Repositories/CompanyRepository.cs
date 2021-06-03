using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;

namespace TeamProject_Manager_Api.Repositories
{
    public interface ICompanyRepository {
        List<Company> GetAllComapnies();
        Company GetComapnyByIdWithAddress(int Id);
        Company GetComapnyById(int Id);
        void CreateCompany(Company company);
        void UpdateComapny(Company company);
        void DeleteComapny(Company company);
        bool ValidComapny(int companyId);
    }

    public class CompanyRepository : ICompanyRepository {

        private readonly ProjectManagerDbContext context;

        public CompanyRepository(ProjectManagerDbContext context) {
            this.context = context;
        }

        public List<Company> GetAllComapnies() {
            return context.Companies
                .Include(c => c.Address)
                .ToList();
        }

        public Company GetComapnyByIdWithAddress(int Id) {
            return context.Companies
                .Include(c => c.Address)
                .SingleOrDefault(c => c.Id == Id);
        }

        public Company GetComapnyById(int Id) {
            return context.Companies
                .SingleOrDefault(c => c.Id == Id);
        }

        public void CreateCompany(Company company) {
            context.Companies.Add(company);
            context.SaveChanges();
        }

        public void UpdateComapny(Company company) {
            context.Update(company);
            context.SaveChanges();
        }

        public void DeleteComapny(Company company) {
            context.Companies.Remove(company);
            context.SaveChanges();
        }

        public bool ValidComapny(int companyId) {
            return context.Companies.Any(c => c.Id == companyId);
        }
    }
}

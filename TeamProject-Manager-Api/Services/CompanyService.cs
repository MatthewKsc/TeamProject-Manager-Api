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
using TeamProject_Manager_Api.Dtos.Models_Operations;

namespace TeamProject_Manager_Api.Services
{
    public interface ICompanyService {

        List<CompanyDTO> GetAllComapnies();
        CompanyDTO GetComapnyById(int Id);
        int CreateCompany(CreatCompany dto);
        void DeleteComapnyById(int Id);
        void UpdateComapny(CreatCompany updatedComapny, int Id);
    }

    public class CompanyService : ICompanyService{

        private readonly ProjectManagerDbContext context;
        private readonly IMapper mapper;

        public CompanyService(ProjectManagerDbContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }

        public List<CompanyDTO> GetAllComapnies() {

            List<Company> companies = context.Companies
                .Include(c => c.Address)
                .ToList();

            if (companies.Count < 1)
                throw new NotFoundException("There is no companies to display");

            var result = mapper.Map<List<CompanyDTO>>(companies);

            return result;
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

        public int CreateCompany(CreatCompany dto) {
            Company company = mapper.Map<Company>(dto);

            context.Companies.Add(company);
            context.SaveChanges();

            return company.Id;
        }
        public void UpdateComapny(CreatCompany updatedComapny, int Id) {
            Company company = context.Companies
                .Include(c=> c.Address)
                .SingleOrDefault(c => c.Id == Id);

            if (company is null)
                throw new NotFoundException($"There is no company with id: {Id}");

            MapUpdatedCompany(company, updatedComapny);

            context.SaveChanges();
        }

        public void DeleteComapnyById(int Id) {
            Company company = context.Companies.SingleOrDefault(c => c.Id == Id);

            if(company is null)
                throw new NotFoundException($"There is no company with id: {Id}");

            context.Companies.Remove(company);
            context.SaveChanges();
        }

        private void MapUpdatedCompany(Company destination, CreatCompany source) {
            destination.CompanyName = source.CompanyName;
            destination.SizeOfComapny = source.SizeOfComapny;
            destination.Address.City = source.City;
            destination.Address.Street = source.Street;
            destination.Address.Country = source.Country;
            destination.Address.PostalCode = source.PostalCode;
        }
    }
}

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
using TeamProject_Manager_Api.Repositories;

namespace TeamProject_Manager_Api.Services
{
    public interface ICompanyService {

        List<CompanyDTO> GetAllComapnies();
        CompanyDTO GetComapnyById(int Id);
        int CreateCompany(CreateCompany dto);
        void DeleteComapnyById(int Id);
        void UpdateComapny(CreateCompany updatedComapny, int Id);
        void ValidCompany(int companyId);
    }

    public class CompanyService : ICompanyService{

        private readonly ICompanyRepository companyRepository;
        private readonly IMapper mapper;

        public CompanyService(ICompanyRepository companyRepository,  IMapper mapper) {
            this.companyRepository = companyRepository;
            this.mapper = mapper;
        }

        public List<CompanyDTO> GetAllComapnies() {

            List<Company> companies = companyRepository.GetAllComapnies();

            if (companies.Count < 1)
                throw new NotFoundException("There is no companies to display");

            var result = mapper.Map<List<CompanyDTO>>(companies);

            return result;
        }

        public CompanyDTO GetComapnyById(int Id) {
            Company company = companyRepository.GetComapnyByIdWithAddress(Id);

            if (company is null)
                throw new NotFoundException($"There is no company with id: {Id}");

            var result = mapper.Map<CompanyDTO>(company);

            return result;
        }

        public int CreateCompany(CreateCompany dto) {
            Company company = mapper.Map<Company>(dto);

            companyRepository.CreateCompany(company);

            return company.Id;
        }
        public void UpdateComapny(CreateCompany updatedComapny, int Id) {
            Company company = companyRepository.GetComapnyByIdWithAddress(Id);

            if (company is null)
                throw new NotFoundException($"There is no company with id: {Id}");

            company = mapper.Map(updatedComapny, company);

            companyRepository.UpdateComapny(company);
        }

        public void DeleteComapnyById(int Id) {
            Company company = companyRepository.GetComapnyById(Id);

            if(company is null)
                throw new NotFoundException($"There is no company with id: {Id}");

            companyRepository.DeleteComapny(company);
        }

        public void ValidCompany(int companyId) {
            if (!companyRepository.ValidComapny(companyId)) {
                throw new BadRequestException($"There is no company with id {companyId}");
            }
        }
    }
}

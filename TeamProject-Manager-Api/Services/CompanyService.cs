using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;

namespace TeamProject_Manager_Api.Services
{
    public interface ICompanyService {

        List<Company> GetAllComapnies();
        Company GetComapnyById();
        int CreateCompany();
        bool DeleteComapnyById();
    }

    public class CompanyService : ICompanyService{

        
        


    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Services;

namespace TeamProject_Manager_Api.Controllers{

    [ApiController] //allows automatic validaction of models
    [Route("api/companies")]
    public class CompanyController : ControllerBase{

        private readonly ICompanyService service;

        public CompanyController(ICompanyService service) {
            this.service = service;
        }

        [HttpGet]
        public ActionResult GetAllComapnies() {
            List<Company> companies = service.GetAllComapnies();

            if (companies.Count < 1)
                return NotFound("There is no companies to display");

            return Ok(companies);
        }

        [HttpGet("{Id}")]
        public ActionResult GetByIt([FromRoute] int Id) {
            Company company = service.GetComapnyById(Id);

            if (company is null) 
                return NotFound($"There is no company with id: {Id}");

            return Ok(company);
        }

        [HttpPost]
        public ActionResult CreateCompany([FromBody] Company company) {
            service.CreateCompany(company);

            if (company.Id == 0)
                return BadRequest("Comapny is not valid"); 

            return Created($"api/companies/{company.Id}", null);
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute]int Id) {
            if(!service.DeleteComapnyById(Id))
                return NotFound($"There is no company with id: {Id}");

            return NoContent();
        }
    }
}

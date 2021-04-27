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
        public ActionResult GetAllComapnies(){

            return Ok(service.GetAllComapnies());
        }

        [HttpGet("{Id}")]
        public ActionResult GetByIt([FromRoute] int Id) {

            return Ok(service.GetComapnyById(Id));
        }

        [HttpPost]
        public ActionResult CreateCompany([FromBody] Company company) {
            service.CreateCompany(company);

            return Created($"api/companies/{company.Id}", null);
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute]int Id) {
            service.DeleteComapnyById(Id);

            return NoContent();
        }
    }
}

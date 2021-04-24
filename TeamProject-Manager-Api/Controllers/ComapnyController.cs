using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;

namespace TeamProject_Manager_Api.Controllers{

    [ApiController] //allows automatic validaction of models
    [Route("api/companies")]
    public class ComapnyController : ControllerBase{

        private readonly ProjectManagerDbContext context;

        //for now to test
        public ComapnyController(ProjectManagerDbContext context) {
            this.context = context;
        }

        [HttpGet]
        public ActionResult GetAllComapnies() {
            return Ok(
                context
                    .Companies
                    .Include(c => c.Address)
                    .Include(c => c.Teams)
                        .ThenInclude(t => t.Projects)
                    .Include(c => c.Teams)
                        .ThenInclude(t => t.TeamMembers)
                    .ToList()
            );
        }

        [HttpGet("{Id}")]
        public ActionResult GetByIt([FromRoute] int Id) {
            return Ok(context.Companies.SingleOrDefault(c => c.Id == Id));
        }

        [HttpPost]
        public ActionResult CreateCompany([FromBody] Company company) {
            context.Companies.Add(company);
            context.SaveChanges();

            return Created($"api/comapny/{company.Id}", null);
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute]int Id) {
            var toRemove = context.Companies.SingleOrDefault(c=> c.Id == Id);
            context.Remove(toRemove);
            context.SaveChanges();

            return NoContent();
        }
    }
}

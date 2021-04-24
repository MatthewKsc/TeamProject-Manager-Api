using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;

namespace TeamProject_Manager_Api.Controllers {
    [ApiController]
    [Route("api/{companyId}/teams")]
    public class TeamController : ControllerBase {

        private readonly ProjectManagerDbContext context;

        public TeamController(ProjectManagerDbContext context) {
            this.context = context;
        }

        [HttpGet]
        public ActionResult GetAll([FromRoute]int companyId) {
            return Ok(context.Teams
                .Where(t => t.CompanyId == companyId)
                .Include(t => t.TeamMembers)
                .Include(t => t.Projects)
                .ToList()
             );
        }

        [HttpGet("{Id}")]
        public ActionResult GetById([FromRoute] int companyId, [FromRoute] int Id) {
            return Ok(context.Teams
                .SingleOrDefault(t => t.CompanyId == companyId && t.Id == Id)
             );
        }

        [HttpPost]
        public ActionResult CreatTeam([FromRoute] int companyId, [FromBody] Team team) {
            team.CompanyId = companyId;
            context.Teams.Add(team);
            context.SaveChanges();

            return Created($"api/{companyId}/teams/{team.Id}", null);
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute] int companyId, [FromRoute] int Id) {
            var team = context.Teams.SingleOrDefault(t => t.CompanyId == companyId && t.Id == Id);
            context.Remove(team);
            context.SaveChanges();

            return NoContent();
        }
    }
}

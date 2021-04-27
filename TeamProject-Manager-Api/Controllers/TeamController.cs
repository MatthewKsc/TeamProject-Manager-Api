using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Services;

namespace TeamProject_Manager_Api.Controllers {

    [ApiController]
    [Route("api/{companyId}/teams")]
    public class TeamController : ControllerBase {

        private readonly ITeamService service;

        public TeamController(ITeamService service) {
            this.service = service;
        }

        [HttpGet]
        public ActionResult GetAll([FromRoute]int companyId) {

            return Ok(service.GetAllTeams(companyId));
        }

        [HttpGet("{Id}")]
        public ActionResult GetById([FromRoute] int companyId, [FromRoute] int Id) {
            
            return Ok(service.GetTeamById(companyId, Id));
        }

        [HttpPost]
        public ActionResult CreatTeam([FromBody] Team team, [FromRoute] int companyId) {
            service.CreateTeam(team, companyId);

            return Created($"api/{companyId}/teams/{team.Id}", null);
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute] int companyId, [FromRoute] int Id) {
            service.DeleteTeamById(companyId, Id);

            return NoContent();
        }
    }
}

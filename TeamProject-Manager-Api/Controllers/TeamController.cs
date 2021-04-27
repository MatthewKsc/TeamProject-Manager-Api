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
            List<Team> teams = service.GetAllTeams(companyId);

            if (teams.Count < 1)
                return NotFound("There is no teams to display");

            return Ok(teams);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById([FromRoute] int companyId, [FromRoute] int Id) {
            Team team = service.GetTeamById(companyId, Id);

            if (team is null)
                NotFound($"There is no team with id: {Id}");

            return Ok(team);
        }

        [HttpPost]
        public ActionResult CreatTeam([FromBody] Team team, [FromRoute] int companyId) {
            service.CreateTeam(team, companyId);

            if (team.Id == 0)
                NotFound("Team is not valid");

            return Created($"api/{companyId}/teams/{team.Id}", null);
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute] int companyId, [FromRoute] int Id) {
            if(!service.DeleteTeamById(companyId, Id))
               return NotFound($"There is no team with id: {Id}");

            return NoContent();
        }
    }
}

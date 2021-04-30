using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.Dtos.Models_Operations;
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
        public ActionResult CreatTeam([FromBody] CreateTeam createTeam, [FromRoute] int companyId) {
            int id = service.CreateTeam(createTeam, companyId);

            return Created($"api/{companyId}/teams/{id}", null);
        }

        [HttpPut("{Id}")]
        public ActionResult UpdateTeam([FromBody] CreateTeam updatedTeam, [FromRoute] int Id) {
            service.UpdateTeam(updatedTeam, Id);

            return Ok();
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute] int companyId, [FromRoute] int Id) {
            service.DeleteTeamById(companyId, Id);

            return NoContent();
        }
    }
}

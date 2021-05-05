using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(Summary = "Retrieves all teams within specified comapny by companyId")]
        public ActionResult GetAll([FromRoute]int companyId) {

            return Ok(service.GetAllTeams(companyId));
        }

        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Retrieve team by provided Id and companyId")]
        public ActionResult GetById([FromRoute] int companyId, [FromRoute] int Id) {
            
            return Ok(service.GetTeamById(companyId, Id));
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new team by specific model and companyId")]
        public ActionResult CreatTeam([FromBody] CreateTeam createTeam, [FromRoute] int companyId) {
            int id = service.CreateTeam(createTeam, companyId);

            return Created($"api/{companyId}/teams/{id}", null);
        }

        [HttpPut("{Id}")]
        [SwaggerOperation(Summary = "Update specific team by model and provided Id")]
        public ActionResult UpdateTeam([FromBody] CreateTeam updatedTeam, [FromRoute] int Id) {
            service.UpdateTeam(updatedTeam, Id);

            return Ok();
        }

        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Delete specific team from api by provided Id ")]
        public ActionResult DeleteById([FromRoute] int companyId, [FromRoute] int Id) {
            service.DeleteTeamById(companyId, Id);

            return NoContent();
        }
    }
}

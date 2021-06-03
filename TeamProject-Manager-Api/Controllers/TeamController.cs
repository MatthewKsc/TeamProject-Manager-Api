using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.Dtos.Models_Operations;
using TeamProject_Manager_Api.Exceptions;
using TeamProject_Manager_Api.Services;

namespace TeamProject_Manager_Api.Controllers {

    [ApiController]
    [Route("api/teams")]
    public class TeamController : ControllerBase {

        private readonly ITeamService service;
        private readonly ICompanyService companyService;

        public TeamController(ITeamService service, ICompanyService companyService) {
            this.service = service;
            this.companyService = companyService;
        }

        [HttpGet("company/{companyId}")]
        [SwaggerOperation(Summary = "Retrieves all teams within specified comapny by companyId")]
        public ActionResult GetAll([FromRoute]int companyId) {
            if (!companyService.ValidCompany(companyId)) {
                throw new BadRequestException($"There is no company with id {companyId}");
            }
            
            return Ok(service.GetAllTeams(companyId));
        }

        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Retrieve team by provided Id")]
        public ActionResult GetById([FromRoute] int Id) {
            
            return Ok(service.GetTeamById(Id));
        }

        [HttpPost("{companyId}")]
        [SwaggerOperation(Summary = "Create a new team by specific model and companyId")]
        public ActionResult CreatTeam([FromBody] CreateTeam createTeam, [FromRoute] int companyId) {

            if (!companyService.ValidCompany(companyId)) {
                throw new BadRequestException($"There is no company with id {companyId}");
            }

            int id = service.CreateTeam(createTeam, companyId);

            return Created($"api/teams/{id}", null);
        }

        [HttpPut("{Id}")]
        [SwaggerOperation(Summary = "Update specific team by model and provided Id")]
        public ActionResult UpdateTeam([FromBody] CreateTeam updatedTeam, [FromRoute] int Id) {
            service.UpdateTeam(updatedTeam, Id);

            return Ok();
        }

        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Delete specific team from api by provided Id ")]
        public ActionResult DeleteById([FromRoute] int Id) {
            service.DeleteTeamById(Id);

            return NoContent();
        }
    }
}

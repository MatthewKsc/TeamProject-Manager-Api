using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Dtos.Models_Operations;
using TeamProject_Manager_Api.Dtos.Querying_Models;
using TeamProject_Manager_Api.Exceptions;
using TeamProject_Manager_Api.Services;

namespace TeamProject_Manager_Api.Controllers {

    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase {

        private readonly IProjectService service;
        private readonly ITeamService teamService;

        public ProjectController(IProjectService service, ITeamService teamService) {
            this.service = service;
            this.teamService = teamService;
        }

        [HttpGet("team/{teamId}")]
        [SwaggerOperation(Summary = "Retrieves projects by query model and specific team by teamId")]
        public ActionResult GetAll([FromBody]Query<ProjectDTO> query, int teamId) {

            teamService.ValidTeam(teamId);

            return Ok(service.GetAllProjects(query, teamId));
        }

        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Retrieve project by provided Id and teamId")]
        public ActionResult GetById([FromRoute] int Id) {

            return Ok(service.GetProjectById(Id));
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new project by specific model and teamId")]
        public ActionResult CreateProject([FromBody] CreateProject createProject, [FromRoute] int teamId) {

            teamService.ValidTeam(teamId);

            int id = service.CreateProject(createProject, teamId);

            return Created($"api/projects/{id}", null);
        }

        [HttpPut("{Id}")]
        [SwaggerOperation(Summary = "Update specific project by model and provided Id")]
        public ActionResult UpdateProject([FromBody] CreateProject updatedProject, [FromRoute] int Id) {
            service.UpdateProject(updatedProject, Id);

            return Ok();
        }

        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Delete specific project from api by provided Id and teamId ")]
        public ActionResult DeleteById([FromRoute] int Id) {
            service.DeleteProjectById(Id);

            return NoContent();
        }
    }
}

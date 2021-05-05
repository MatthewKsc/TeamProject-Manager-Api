using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Dtos.Models_Operations;
using TeamProject_Manager_Api.Dtos.Querying_Models;
using TeamProject_Manager_Api.Services;

namespace TeamProject_Manager_Api.Controllers {

    [ApiController]
    [Route("api/{teamId}/projects")]
    public class ProjectController : ControllerBase {

        private readonly IProjectService service;

        public ProjectController(IProjectService service) {
            this.service = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves projects by query model and specific team by teamId")]
        public ActionResult GetAll([FromBody]Query<ProjectDTO> query, [FromRoute] int teamId) {

            return Ok(service.GetAllProjects(query, teamId));
        }

        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Retrieve project by provided Id and teamId")]
        public ActionResult GetById([FromRoute] int teamId, [FromRoute] int Id) {

            return Ok(service.GetProjectById(teamId, Id));
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new project by specific model and teamId")]
        public ActionResult CreateProject([FromBody] CreateProject createProject, [FromRoute] int teamId) {
            int id = service.CreateProject(createProject, teamId);

            return Created($"api/{teamId}/projects/{id}", null);
        }

        [HttpPut("{Id}")]
        [SwaggerOperation(Summary = "Update specific project by model and provided Id")]
        public ActionResult UpdateProject([FromBody] CreateProject updatedProject, [FromRoute] int Id) {
            service.UpdateProject(updatedProject, Id);

            return Ok();
        }

        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Delete specific project from api by provided Id and teamId ")]
        public ActionResult DeleteById([FromRoute] int teamId, [FromRoute] int Id) {
            service.DeleteProjectById(teamId, Id);

            return NoContent();
        }
    }
}

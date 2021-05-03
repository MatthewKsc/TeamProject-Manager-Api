using Microsoft.AspNetCore.Mvc;
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
        public ActionResult GetAll([FromBody]Query<ProjectDTO> query, [FromRoute] int teamId) {

            return Ok(service.GetAllProjects(query, teamId));
        }

        [HttpGet("{Id}")]
        public ActionResult GetById([FromRoute] int teamId, [FromRoute] int Id) {

            return Ok(service.GetProjectById(teamId, Id));
        }

        [HttpPost]
        public ActionResult CreateProject([FromBody] CreateProject createProject, [FromRoute] int teamId) {
            int id = service.CreateProject(createProject, teamId);

            return Created($"api/{teamId}/projects/{id}", null);
        }

        [HttpPut("{Id}")]
        public ActionResult UpdateProject([FromBody] CreateProject updatedProject, [FromRoute] int Id) {
            service.UpdateProject(updatedProject, Id);

            return Ok();
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute] int teamId, [FromRoute] int Id) {
            service.DeleteProjectById(teamId, Id);

            return NoContent();
        }
    }
}

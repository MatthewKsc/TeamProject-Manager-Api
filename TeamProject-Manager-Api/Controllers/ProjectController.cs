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
    [Route("api/{teamId}/projects")]
    public class ProjectController : ControllerBase {

        private readonly IProjectService service;

        public ProjectController(IProjectService service) {
            this.service = service;
        }

        [HttpGet]
        public ActionResult GetAll([FromRoute] int teamId) {

            return Ok(service.GetAllProjects(teamId));
        }

        [HttpGet("{Id}")]
        public ActionResult GetById([FromRoute] int teamId, [FromRoute] int Id) {

            return Ok(service.GetProjectById(teamId, Id));
        }

        [HttpPost]
        public ActionResult CreateProject([FromBody] Project project, [FromRoute] int teamId) {
            service.CreateProject(project, teamId);

            return Created($"api/{teamId}/projects/{project.Id}", null);
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute] int teamId, [FromRoute] int Id) {
            service.DeleteProjectById(teamId, Id);

            return NoContent();
        }
    }
}

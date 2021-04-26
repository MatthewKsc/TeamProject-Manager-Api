using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
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
            List<Project> projects = service.GetAllProjects(teamId);

            if (projects.Count < 1)
                return NotFound("There is no projects to display");

            return Ok(projects);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById([FromRoute] int teamId, [FromRoute] int Id) {
            Project project = service.GetProjectById(teamId, Id);

            if (project is null)
                return NotFound($"There is no project with id: {Id}");

            return Ok(project);
        }

        [HttpPost]
        public ActionResult CreateProject([FromBody] Project project, [FromRoute] int teamId) {
            service.CreateProject(project, teamId);

            if (project.Id == 0)
                NotFound("Project is not valid");

            return Created($"api/{teamId}/projects/{project.Id}", null);
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute] int teamId, [FromRoute] int Id) {
            if (!service.DeleteProjectById(teamId, Id))
                return NotFound($"There is no project with id: {Id}");

            return NoContent();
        }
    }
}

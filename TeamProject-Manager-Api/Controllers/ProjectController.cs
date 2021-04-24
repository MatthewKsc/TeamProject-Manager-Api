using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;
using TeamProject_Manager_Api.dao.Entitys;

namespace TeamProject_Manager_Api.Controllers{

    [ApiController]
    [Route("api/{teamId}/projects")]
    public class ProjectController : ControllerBase{

        private readonly ProjectManagerDbContext context;

        public ProjectController(ProjectManagerDbContext context) {
            this.context = context;
        }

        [HttpGet]
        public ActionResult GetAll([FromRoute] int teamId) {
            return Ok(context
                .Projects
                .Where(p => p.OwnerTeamId == teamId)
                .Include(t => t.UsersAssigned)
                .Include(t => t.OwnerTeam)
                .ToList()
           );
        }

        [HttpGet("{Id}")]
        public ActionResult GetById([FromRoute] int teamId, [FromRoute] int Id) {
            return Ok(context.Projects
                .SingleOrDefault( p => p.OwnerTeamId == teamId && p.Id == Id)
            );
        }

        [HttpPost]
        public ActionResult CreateProject([FromBody] Project project, [FromRoute]int teamId) {
            project.OwnerTeamId = teamId;
            context.Add(project);
            context.SaveChanges();

            return Created($"api/{teamId}/projects/{project.Id}", null);
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute]int teamId, [FromRoute] int Id) {
            var project = context.Projects.SingleOrDefault(p => p.OwnerTeamId == teamId && p.Id == Id);
            context.Remove(project);
            context.SaveChanges();

            return NoContent();
        }
    }
}

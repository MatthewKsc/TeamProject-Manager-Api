using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.Services;

namespace TeamProject_Manager_Api.Controllers
{
    [ApiController]
    [Route ("api/manage_project")]
    public class UserProjectsController : ControllerBase{

        private readonly IUserProjectsService service;

        public UserProjectsController(IUserProjectsService service) {
            this.service = service;
        }

        [HttpPost("add")]
        public ActionResult AddUserToProject([FromBody]string userEmail,[FromQuery] int projectId) {
            service.AddUserToProject(userEmail, projectId);

            return Ok();
        }

        [HttpPost("add_list")]
        public ActionResult AddUserToProject([FromBody]List<string> usersEmail, [FromQuery] int projectId) {
            service.AddUserToProject(usersEmail, projectId);

            return Ok();
        }

        [HttpDelete("remove")]
        public ActionResult RemoveUserFromProject([FromBody] string userEmail, [FromQuery] int projectId) {
            service.RemoveUserFromProject(userEmail, projectId);

            return NoContent();
        }
    }
}

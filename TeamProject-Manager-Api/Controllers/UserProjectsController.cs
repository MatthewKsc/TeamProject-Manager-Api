using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(Summary = "Adding user by user email to specific project by projectId")]
        public ActionResult AddUserToProject([FromBody]string userEmail,[FromQuery] int projectId) {
            service.AddUserToProject(userEmail, projectId);

            return Ok();
        }

        [HttpPost("add_list")]
        [SwaggerOperation(Summary = "Adding many users by list of users emails to specific project by projectId")]
        public ActionResult AddUserToProject([FromBody]List<string> usersEmail, [FromQuery] int projectId) {
            service.AddUserToProject(usersEmail, projectId);

            return Ok();
        }

        [HttpDelete("remove")]
        [SwaggerOperation(Summary = "Remove user by user email from specific project by projectId")]
        public ActionResult RemoveUserFromProject([FromBody] string userEmail, [FromQuery] int projectId) {
            service.RemoveUserFromProject(userEmail, projectId);

            return NoContent();
        }
    }
}

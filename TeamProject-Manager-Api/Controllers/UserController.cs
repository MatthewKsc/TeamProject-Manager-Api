using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Dtos.Models_Operations;
using TeamProject_Manager_Api.Dtos.Querying_Models;
using TeamProject_Manager_Api.Services;

namespace TeamProject_Manager_Api.Controllers{

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase{
        
        private readonly IUserService service;
        private readonly ITeamService teamService;

        public UserController(IUserService service, ITeamService teamService) {
            this.service = service;
            this.teamService = teamService;
        }

        [HttpGet("team/{teamId}")]
        [SwaggerOperation(Summary = "Retrieves users by query model and specific team by teamId")]
        public ActionResult GetAll([FromBody] Query<UserDTO> query, [FromRoute] int teamId) {

            teamService.ValidTeam(teamId);

            return Ok(service.GetAllUsers(query ,teamId));
        }

        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Retrieve user by provided Id")]
        public ActionResult GetById([FromRoute] int Id) {
            
            return Ok(service.GetUserById(Id));
        }

        [HttpPost("{teamId}")]
        [SwaggerOperation(Summary = "Create a new user by specific model and teamId")]
        public ActionResult CreatUser([FromBody] CreateUser createUser, [FromRoute] int teamId) {

            teamService.ValidTeam(teamId);

            int id = service.CreateUser(createUser, teamId);

            return Created($"api/users/{id}", null);
        }

        [HttpPut("{Id}")]
        [SwaggerOperation(Summary = "Update specific user by model and provided Id")]
        public ActionResult UpdateUser([FromBody] CreateUser updatedUser, [FromRoute] int Id) {
            service.UpdateUser(updatedUser, Id);

            return Ok();
        }

        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Delete specific user from api by provided Id")]
        public ActionResult DeleteById( [FromRoute] int Id) {
            service.DeleteUserById(Id);

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.Dtos.Models_Operations;
using TeamProject_Manager_Api.Services;

namespace TeamProject_Manager_Api.Controllers{

    [ApiController]
    [Route("api/{teamId}/users")]
    public class UserController : ControllerBase{
        
        private readonly IUserService service;

        public UserController(IUserService service) {
            this.service = service;
        }

        [HttpGet]
        public ActionResult GetAll([FromRoute] int teamId) {

            return Ok(service.GetAllUsers(teamId));
        }

        [HttpGet("{Id}")]
        public ActionResult GetById([FromRoute] int teamId, [FromRoute] int Id) {
            
            return Ok(service.GetUserById(teamId, Id));
        }

        [HttpPost]
        public ActionResult CreatUser([FromBody] CreateUser createUser, [FromRoute] int teamId) {
            int id = service.CreateUser(createUser, teamId);

            return Created($"api/{teamId}/teams/{id}", null);
        }

        [HttpPut("{Id}")]
        public ActionResult UpdateUser([FromBody] CreateUser updatedUser, [FromRoute] int Id) {
            service.UpdateUser(updatedUser, Id);

            return Ok();
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute] int teamId, [FromRoute] int Id) {
            service.DeleteUserById(teamId, Id);

            return NoContent();
        }
    }
}

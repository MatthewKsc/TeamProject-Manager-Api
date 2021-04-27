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

namespace TeamProject_Manager_Api.Controllers{

    [ApiController]
    [Route("api/{teamId}/users")]
    public class UserController : ControllerBase{
        
        private readonly UserService service;

        public UserController(UserService service) {
            this.service = service;
        }

        [HttpGet]
        public ActionResult GetAll([FromRoute] int teamId) {
            List<User> users = service.GetAllUsers(teamId);

            if (users.Count < 1)
                return NotFound("There is no users to display");

            return Ok(users);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById([FromRoute] int teamId, [FromRoute] int Id) {
            User user = service.GetUserById(teamId, Id);

            if (user is null)
                NotFound($"There is no user with id: {Id}");

            return Ok(user);
        }

        [HttpPost]
        public ActionResult CreatUser([FromBody] User user, [FromRoute] int teamId) {
            service.CreateUser(user, teamId);

            if (user.Id == 0)
                NotFound("Team is not valid");

            return Created($"api/{teamId}/teams/{user.Id}", null);
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute] int teamId, [FromRoute] int Id) {
            if(!service.DeleteUserById(teamId, Id))
                return NotFound($"There is no user with id: {Id}");

            return NoContent();
        }
    }
}

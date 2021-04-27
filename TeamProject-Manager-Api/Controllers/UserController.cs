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
        public ActionResult CreatUser([FromBody] User user, [FromRoute] int teamId) {
            service.CreateUser(user, teamId);

            return Created($"api/{teamId}/teams/{user.Id}", null);
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute] int teamId, [FromRoute] int Id) {
            service.DeleteUserById(teamId, Id);

            return NoContent();
        }
    }
}

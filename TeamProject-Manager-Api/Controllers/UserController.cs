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
    [Route("api/{teamId}/users")]
    public class UserController : ControllerBase{

        private readonly ProjectManagerDbContext context;

        public UserController(ProjectManagerDbContext context) {
            this.context = context;
        }

        [HttpGet]
        public ActionResult GetAll([FromRoute] int teamId) {
            return Ok(context.Users
                .Where(u => u.TeamId == teamId)
                .Include(u=> u.Address)
                .ToList()
           );
        }

        [HttpGet("{Id}")]
        public ActionResult GetById([FromRoute] int teamId, [FromRoute] int Id) {
            return Ok(context.Users
                .SingleOrDefault(u => u.TeamId==teamId && u.Id == Id)
            );
        }

        [HttpPost]
        public ActionResult CreatUser([FromRoute] int teamId, [FromBody] User user) {
            user.TeamId = teamId;
            context.Users.Add(user);
            context.SaveChanges();

            return Created($"api/{teamId}/teams/{user.Id}", null);
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteById([FromRoute] int teamId, [FromRoute] int Id) {
            var user = context.Users
                        .SingleOrDefault(u => u.TeamId == teamId && u.Id == Id);
            context.Users.Remove(user);
            context.SaveChanges();

            return NoContent();
        }
    }
}

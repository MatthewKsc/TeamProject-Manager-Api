using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;

namespace TeamProject_Manager_Api.Controllers{

    [ApiController] //allows automatic validaction of models
    [Route("api/companies")]
    public class ComapnyController : ControllerBase{

        private readonly ProjectManagerDbContext context;

        //for now to test
        public ComapnyController(ProjectManagerDbContext context) {
            this.context = context;
        }

        [HttpGet]
        public ActionResult GetAllComapnies() {
            return Ok(
                context
                    .Companies
                    .Include(c=> c.Address)
                    .Include(c=> c.Teams)
                    .ToList()
            );
        }
    }
}

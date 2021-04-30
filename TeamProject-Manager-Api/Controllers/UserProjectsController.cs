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
    [Route ("api/adduser")]
    public class UserProjectsController : ControllerBase{

        private readonly IUserProjectsService service;

        public UserProjectsController(IUserProjectsService service) {
            this.service = service;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;

namespace TeamProject_Manager_Api.Services
{

    interface IAddUserToEntityService {

    }

    public class AddUserToEntityService: IAddUserToEntityService{

        private readonly ProjectManagerDbContext context;

        public AddUserToEntityService(ProjectManagerDbContext context) {
            this.context = context;
        }
    }
}

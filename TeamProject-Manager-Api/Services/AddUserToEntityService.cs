using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao;

namespace TeamProject_Manager_Api.Services
{

    public interface IAddUserToEntityService {

        void AddUserToProject(string userEmail, int projectId);
        void AddUserToProject(List<string> userEmail, int projectId);
        void AddUserToTeam(string userEmail, int teamId);
    }

    public class AddUserToEntityService: IAddUserToEntityService{

        private readonly ProjectManagerDbContext context;

        public AddUserToEntityService(ProjectManagerDbContext context) {
            this.context = context;
        }

        public void AddUserToProject(string userEmail, int projectId) {
            throw new NotImplementedException();
        }

        public void AddUserToProject(List<string> userEmail, int projectId) {
            throw new NotImplementedException();
        }

        public void AddUserToTeam(string userEmail, int teamId) {
            throw new NotImplementedException();
        }
    }
}

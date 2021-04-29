using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.dao.Entitys
{
    public class UserProject
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public static List<UserProject> AddManyUsersToProject(List<User> users, Project project) {
            List<UserProject> result = new List<UserProject>();

            foreach(var item in users) {
                result.Add(new UserProject { User = item, Project = project });
            }

            return result;
        }
    }
}

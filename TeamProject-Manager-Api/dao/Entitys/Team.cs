using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.dao.Entitys {
    public class Team {

        public int Id { get; set; }
        public string NameOfTeam { get; set; }
        public int CompnayId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<User> TeamMembers { get; set; }
        public virtual ICollection<Project> Projects{ get; set; }

        public Team() {

        }

        public Team(string name, Company company, ICollection<User> users, ICollection<Project> projects) {
            this.NameOfTeam = name;
            this.Company = company;
            this.TeamMembers = users;
            this.Projects = projects;
        }
    }
}

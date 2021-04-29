using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.dao.Entitys {
    public class Project {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartOfProject { get; set; }
        public DateTime? EndOfProject { get; set; }
        public virtual ICollection<UserProject> UserProjects { get; set; }
        public int OwnerTeamId { get; set; }
        public virtual Team OwnerTeam{ get; set; }

        public Project() {

        }

        public Project(string title, string desc, Team team) {
            this.Title = title;
            this.Description = desc;
            this.OwnerTeam = team;
        }

        public void SetDatesOfProject(DateTime start, DateTime end) {
            this.StartOfProject = start;
            this.EndOfProject = end;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.Dtos.Models
{
    public class ProjectDTO{

        public int Id { get; set; }
        public string ResponsibleTeam { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartOfProject { get; set; }
        public DateTime? EndOfProject { get; set; }
        public ICollection<UserDTO> UsersAssigned { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.Dtos.Models
{
    public class TeamDTO{

        public int Id { get; set; }
        public string NameOfTeam { get; set; }
        public ICollection<UserDTO> Users { get; set; }
    }
}

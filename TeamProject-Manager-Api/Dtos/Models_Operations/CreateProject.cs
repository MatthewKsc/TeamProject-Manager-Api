using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.Dtos.Models_Operations
{
    public class CreateProject{
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartOfProject { get; set; }
        public DateTime? EndOfProject { get; set; }
    }
}

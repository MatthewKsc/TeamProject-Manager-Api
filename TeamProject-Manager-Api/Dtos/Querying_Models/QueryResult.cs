using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.Dtos.Querying_Models
{
    public class Query<T>{
        
        public string searchPhrase { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }

    }
}

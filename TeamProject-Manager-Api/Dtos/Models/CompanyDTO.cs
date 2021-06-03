using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.Dtos.Models
{
    public class CompanyDTO{

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int SizeOfComapny { get; set; }
        public AddressDTO Address { get; set; }
    }
}

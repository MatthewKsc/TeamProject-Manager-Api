using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.dao.Entitys {
    public class Company {

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int SizeOfComapny { get; set; }
        public Address Address { get; set; }
        public ICollection<Team> Teams{get; set;}

        public Company() {

        }

        public Company(string companyName, int sizeComapny, Address address, ICollection<Team> teams) {
            this.CompanyName = companyName;
            this.SizeOfComapny = sizeComapny;
            this.Address = address;
            this.Teams = teams;
        }
    }
}

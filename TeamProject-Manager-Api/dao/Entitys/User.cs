using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.dao.Entitys {
    public class User {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth{ get; set; }
        public int AddressId{ get; set; }
        public virtual Address Address { get; set;}
        public int TeamId{ get; set; }
        public virtual Team Team { get; set; }

        //in future Role and Password for login and based on rola authorization

        public User() {

        }

        public User(string firstName, string lastName, string Email, Address address, Team team) {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = Email;
            this.Address = address;
            this.Team = team;
        }

        public string GetFullName() {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}

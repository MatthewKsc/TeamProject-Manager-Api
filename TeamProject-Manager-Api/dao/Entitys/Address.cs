using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.dao.Entitys{
    public class Address{

        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

        public Address() {

        }

        public Address(string country, string city, string street, string postalCode) {
            this.Country = country;
            this.City = city;
            this.Street = street;
            this.PostalCode = postalCode;
        }
    }
}

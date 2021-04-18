using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.dao.Entitys{
    public class Address{

        public int Id { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

        public Address() {

        }

        public Address(string Country, string Street, string PostalCode) {
            this.Country = Country;
            this.Street = Street;
            this.PostalCode = PostalCode;
        }
    }
}

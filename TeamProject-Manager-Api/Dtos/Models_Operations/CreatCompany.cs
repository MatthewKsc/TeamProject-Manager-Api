﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.Dtos.Models_Operations
{
    public class CreatCompany{

        public string CompanyName { get; set; }
        public int SizeOfComapny { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

    }
}
